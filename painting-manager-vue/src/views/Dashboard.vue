<template>
  <BaseLayout>
    <div class="space-y-4">
      <h1 class="text-2xl font-bold">Dashboard</h1>
      <p>Bem-vindo! Token: {{ token }}</p>
      <p>Role: {{ role }}</p>
    </div>
  </BaseLayout>
</template>

<script setup>
import BaseLayout from '../layouts/BaseLayout.vue'
import { useAuthStore } from '../stores/auth'

const auth = useAuthStore()
const token = auth.token
const role = auth.role
</script>
