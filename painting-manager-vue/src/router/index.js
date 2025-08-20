import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import Dashboard from '../views/Dashboard.vue'
import { useAuthStore } from '../stores/auth'

const routes = [
  { path: '/login', component: Login },
  { path: '/', component: Dashboard }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// Proteção de rotas
router.beforeEach((to, from, next) => {
  const auth = useAuthStore()
  if (to.path !== '/login' && !auth.token) next('/login')
  else next()
})

export default router
