<template>
  <div class="layout">
    <!-- Header -->
    <header class="header">
      <div class="logo">
        <img src="/images/logo_apt_bg.png" alt="Logo" />
      </div>

      <nav class="menu">
        <button
          v-for="(group, index) in groups"
          :key="group.id"
          @click="navigate(group.path)"
          class="menu-item"
          :style="{ '--hover-color': colors[index % colors.length] }"
        >
          <component :is="icons[group.id] || Folder" class="icon" />
          <span>{{ group.name }}</span>
        </button>
      </nav>

      <!-- Botão de logout à direita -->
      <div class="logout">
        <button @click="logout" class="logout-button">
          Logout
        </button>
      </div>
    </header>

    <!-- Conteúdo -->
    <main class="content">
      <slot />
    </main>
  </div>
</template>

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

// Cores de hover para cada menu
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

const navigate = (path) => {
  if (path) router.push(path)
}

const logout = () => {
  localStorage.clear()
  sessionStorage.clear()
  router.push('/login')
}
</script>

<style scoped>
.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

/* Header horizontal */
.header {
  display: flex;
  align-items: center;
  background-color: #1f2937;
  color: white;
  padding: 16px;
}

/* Logo */
.logo img {
  width: 64px;
  height: auto;
  border-radius: 8px;
  background-color: white;
  padding: 4px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.2);
}

/* Menu horizontal distribuído */
.menu {
  display: flex;
  flex: 1;
  justify-content: center; /* menus centralizados */
  gap: 32px; /* espaço entre menus */
  margin-left: 32px;
}

.menu-item {
  display: flex;
  align-items: center;
  gap: 8px;
  background: none;
  border: none;
  color: white; /* cor padrão branca */
  cursor: pointer;
  font-size: 16px;
  transition: color 0.2s, transform 0.2s;
}

/* Hover colorido */
.menu-item:hover {
  color: var(--hover-color);
  transform: scale(1.1); /* leve zoom no hover */
}

/* Botão de logout */
.logout {
  margin-left: auto; /* empurra para a direita */
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

/* Ícones */
.icon {
  width: 20px;
  height: 20px;
}

/* Conteúdo principal */
.content {
  flex: 1;
  padding: 32px;
  background-color: #f3f4f6;
}
</style>
