<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { Palette, PaintBucket, Layers, Folder } from 'lucide-vue-next'

const router = useRouter()
const groups = ref([])

const icons = {
  2: Palette,
  3: PaintBucket,
  4: Layers
}

const colors = ['#f87171', '#facc15', '#34d399', '#60a5fa', '#a78bfa', '#f472b6']

onMounted(async () => {
  try {
    const response = await fetch('http://localhost:5029/api/groups/list')
    if (!response.ok) throw new Error('Erro ao carregar menus')
    groups.value = await response.json()
  } catch (error) {
    console.error(error)
  }
})

// Navegação para grupo
const navigate = (id) => {
  router.push(`/group/${id}`)
}

// Logout
const logout = () => {
  localStorage.clear()
  sessionStorage.clear()
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

      <nav class="menu">
        <button
          v-for="(group, index) in groups"
          :key="group.id"
          @click="navigate(group.id)"
          class="menu-item"
          :style="{ '--hover-color': colors[index % colors.length] }"
        >
          <component :is="icons[group.id] || Folder" class="icon" />
          <span>{{ group.name }}</span>
        </button>
      </nav>

      <div class="logout">
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
</style>
