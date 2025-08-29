<script setup>
import BaseLayout from '../layouts/BaseLayout.vue'
import { useAuthStore } from '../stores/auth'
import { ref, onMounted } from 'vue'
import axios from 'axios'
import FormulaCard from '../components/FormulaCard.vue'

const auth = useAuthStore()
const token = auth.token

const formulas = ref([])

const fetchFormulas = async () => {
  try {
    const response = await axios.get('http://localhost:5029/api/monted/list', {
      headers: { Authorization: `Bearer ${token}` }
    })
    formulas.value = response.data
  } catch (error) {
    console.error('Erro ao buscar fórmulas:', error)
  }
}

onMounted(() => {
  fetchFormulas()
})
</script>

<template>
  <BaseLayout>
    <div class="dashboard">
      <h1>Dashboard</h1>
      <p>Bem-vindo!</p>

      <div class="formulas-section">
        <h2>Fórmulas</h2>

        <div v-if="formulas.length === 0" class="empty">Nenhuma fórmula encontrada.</div>

        <div v-else class="grid">
          <FormulaCard v-for="(f, index) in formulas" :key="index" :formula="f" />
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<style>
.dashboard {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
  font-family: Arial, sans-serif;
}

h1 {
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 10px;
}

h2 {
  font-size: 22px;
  margin-bottom: 15px;
}

.formulas-section .grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 20px;
}

.empty {
  color: #666;
  font-style: italic;
}
</style>
