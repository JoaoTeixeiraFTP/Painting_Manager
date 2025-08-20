import { defineStore } from 'pinia'
import axios from 'axios'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('jwt') || '',
    role: localStorage.getItem('role') || '',
  }),
  getters: {
    isLoggedIn: (state) => !!state.token,
  },
  actions: {
    async login(username, password) {
      const resp = await axios.post(`${import.meta.env.VITE_API_URL}/login`, {
        username,
        password
      })
      this.token = resp.data.token
      this.role = resp.data.role
      localStorage.setItem('jwt', this.token)
      localStorage.setItem('role', this.role)
    },
    logout() {
      this.token = ''
      this.role = ''
      localStorage.removeItem('jwt')
      localStorage.removeItem('role')
    }
  }
})
