import { defineStore } from 'pinia'
import axios from 'axios'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: null,
    role: null
  }),
  actions: {
    async login(username, password) {
      const resp = await axios.post('http://localhost:5029/api/auth/login', { username, password })
      if (resp.status !== 200) throw new Error('Login failed')
      this.token = resp.data.token
      this.role = resp.data.role
    },
    logout() {
      this.token = null
      this.role = null
    }
  }
})
