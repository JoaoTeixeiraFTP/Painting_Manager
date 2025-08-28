<script setup>
import BaseLayout from '../layouts/BaseLayout.vue'
import { useAuthStore } from '../stores/auth'
import { ref, onMounted } from 'vue'
import axios from 'axios'

const auth = useAuthStore()
const token = auth.token
const role = auth.role

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
          <div v-for="(f, index) in formulas" :key="index" class="card">
            <p class="titulo">{{ f.titulo }}</p>
            <p class="grupo">{{ f.grupo }}</p>
            <p class="cliente"><strong>Cliente:</strong> {{ f.cliente }}</p>

            <div v-if="f.formula.length > 0" class="componentes">
              <p class="componentes-title">Componentes:</p>
              <ul>
                <li v-for="(line, idx) in f.formula" :key="idx">
                  {{ line.componente }} - {{ line.quantidade }}
                </li>
              </ul>
            </div>
            <div v-else class="no-componentes">
              <em>Sem componentes</em>
            </div>
          </div>
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

.card {
  background-color: #fff;
  border-radius: 8px;
  padding: 15px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.15);
  transition: box-shadow 0.3s ease;
}

.card:hover {
  box-shadow: 0 4px 12px rgba(0,0,0,0.25);
}

.titulo {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 5px;
}

.grupo {
  color: #555;
  margin-bottom: 5px;
}

.cliente {
  margin-bottom: 10px;
}

.componentes-title {
  font-weight: bold;
  margin-bottom: 5px;
}

.componentes ul {
  list-style-type: disc;
  padding-left: 20px;
  margin: 0;
}

.no-componentes {
  font-style: italic;
  color: #888;
}

.empty {
  color: #666;
  font-style: italic;
}
</style>
