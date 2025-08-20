<template>
  <v-container>
    <h1>Dashboard</h1>
    <p>Bem-vindo! Token: {{ token }}</p>
    <p>Role: {{ role }}</p>
    <v-btn color="red" @click="logout">Logout</v-btn>
  </v-container>
</template>

<script setup>
import { useAuthStore } from '../stores/auth'
import { useRouter } from 'vue-router'

const auth = useAuthStore()
const router = useRouter()

const token = auth.token
const role = auth.role

const logout = () => {
  auth.logout()
  router.push('/login')
}
</script>
