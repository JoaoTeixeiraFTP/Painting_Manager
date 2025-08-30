<script setup>
import { ref, onMounted, computed } from 'vue'
import { useRouter } from 'vue-router'
import { Palette, PaintBucket, Layers, Folder, Search } from 'lucide-vue-next'
import { useAuthStore } from '../stores/auth'

const router = useRouter()
const auth = useAuthStore()
const groups = ref([])
const colors = ref([])

const searchQuery = ref("")
const searchResults = computed(() => {
  if (!searchQuery.value) return []
  return colors.value.filter(c =>
    c.code.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
    c.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
})

const icons = { 2: Palette, 3: PaintBucket, 4: Layers }
const menuColors = ['#f87171', '#facc15', '#34d399', '#60a5fa', '#a78bfa', '#f472b6']

onMounted(async () => {
  try {
    const [groupsResponse, colorsResponse] = await Promise.all([
      fetch('http://localhost:5029/api/groups/list'),
      fetch('http://localhost:5029/api/colors/list')
    ])
    if (!groupsResponse.ok || !colorsResponse.ok) throw new Error('Erro ao carregar dados')
    groups.value = await groupsResponse.json()
    colors.value = await colorsResponse.json()
  } catch (error) {
    console.error(error)
  }
})

const navigate = (id) => router.push(`/group/${id}`)

const goToColor = (color) => {
  searchQuery.value = ""
  // Usa 'code', que corresponde ao valor que a API espera
  router.push(`/formula/${encodeURIComponent(color.code)}`)
}


const logout = () => {
  auth.logout()
  router.push('/login')
}
</script>

<template>
  <div class="layout">
    <header class="header">
      <div class="logo">
        <a href="/dashboard">
          <img src="/images/logo_apt_bg.png" alt="Logo" />
        </a>
      </div>

      <!-- MENU PRINCIPAL -->
      <nav class="menu">
        <button
          v-for="(group, index) in groups"
          :key="group.id"
          @click="navigate(group.id)"
          class="menu-item"
          :style="{ '--hover-color': menuColors[index % menuColors.length] }"
        >
          <component :is="icons[group.id] || Folder" class="icon" />
          <span>{{ group.name }}</span>
        </button>
      </nav>

      <!-- SEARCH BAR -->
      <div class="search-container">
        <Search class="search-icon" />
        <input
          type="text"
          v-model="searchQuery"
          placeholder="Pesquisar cor..."
          class="search-input"
        />
        <ul v-if="searchResults.length" class="search-results">
          <li
            v-for="color in searchResults"
            :key="color.id"
            @click="goToColor(color)"
            class="search-item"
          >
            <strong>{{ color.name }}</strong> - {{ color.code }}
          </li>
        </ul>
      </div>

      <!-- AÇÕES -->
      <div class="logout">
        <button @click="router.push('/new-formula')" class="new-formula-button">
          Nova Fórmula
        </button>
        <button @click="logout" class="logout-button">Logout</button>
      </div>
    </header>

    <main class="content">
      <slot />
    </main>
  </div>
</template>

<style scoped>
.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.header {
  display: flex;
  align-items: center;
  background-color: #1f2937;
  color: white;
  padding: 16px;
  position: relative;
}

.logo img {
  width: 64px;
  height: auto;
  border-radius: 8px;
  background-color: white;
  padding: 4px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.2);
}

.menu {
  display: flex;
  flex: 1;
  justify-content: center;
  gap: 32px;
  margin-left: 32px;
}

.menu-item {
  display: flex;
  align-items: center;
  gap: 8px;
  background: none;
  border: none;
  color: white;
  cursor: pointer;
  font-size: 16px;
  transition: color 0.2s, transform 0.2s;
}

.menu-item:hover {
  color: var(--hover-color);
  transform: scale(1.1);
}

/* SEARCH BAR */
.search-container {
  position: relative;
  margin-right: 24px;
}
.search-input {
  padding: 6px 12px 6px 32px;
  border-radius: 6px;
  border: none;
  outline: none;
  font-size: 14px;
  color: white;
}
.search-icon {
  position: absolute;
  left: 8px;
  top: 50%;
  transform: translateY(-50%);
  width: 16px;
  height: 16px;
  color: #6b7280;
}
.search-results {
  position: absolute;
  top: 110%;
  left: 0;
  right: 0;
  background: white;
  color: black;
  border-radius: 6px;
  max-height: 200px;
  overflow-y: auto;
  box-shadow: 0 4px 6px rgba(0,0,0,0.2);
  z-index: 50;
}
.search-item {
  padding: 8px 12px;
  cursor: pointer;
  border-bottom: 1px solid #e5e7eb;
}
.search-item:hover {
  background: #f3f4f6;
}

.logout {
  margin-left: auto;
}

.logout-button {
  background: #ef4444;
  color: white;
  border: none;
  padding: 8px 16px;
  border-radius: 6px;
  cursor: pointer;
  font-weight: bold;
}

.logout-button:hover {
  background: #f87171;
}

.icon {
  width: 20px;
  height: 20px;
}

.content {
  flex: 1;
  padding: 32px;
  background-color: #f3f4f6;
}

.new-formula-button {
  background: #3b82f6;
  color: white;
  border: none;
  padding: 8px 16px;
  border-radius: 6px;
  cursor: pointer;
  font-weight: bold;
  margin-right: 12px;
}

.new-formula-button:hover {
  background: #60a5fa;
}
</style>
