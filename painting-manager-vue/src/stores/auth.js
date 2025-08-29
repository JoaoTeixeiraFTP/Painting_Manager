import { defineStore } from 'pinia'
import axios from 'axios'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('authToken') || null,
    role: localStorage.getItem('authRole') || null
  }),
  actions: {
    async login(username, password) {
      const resp = await axios.post('http://localhost:5029/api/auth/login', { username, password })
      if (resp.status !== 200) throw new Error('Login failed')
      this.token = resp.data.token
      this.role = resp.data.role
      localStorage.setItem('authToken', this.token)
      localStorage.setItem('authRole', this.role)
    },
    logout() {
      this.token = null
      this.role = null
      localStorage.removeItem('authToken')
      localStorage.removeItem('authRole')
    }
  }
})
