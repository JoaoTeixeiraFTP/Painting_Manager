<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import BaseLayout from '../layouts/BaseLayout.vue'

const route = useRoute()
const formulas = ref([])

// Decodifica o título da URL
const titulo = decodeURIComponent(route.params.titulo)

onMounted(async () => {
  try {
    const response = await fetch(`http://localhost:5029/api/monted/formula/${titulo}`)
    if (!response.ok) throw new Error('Erro ao carregar a fórmula')
    formulas.value = await response.json()
  } catch (error) {
    console.error(error)
  }
})
</script>

<template>
  <BaseLayout>
    <div v-if="formulas.length">
      <div v-for="(formula, index) in formulas" :key="index" class="formula-card">
        <h1>{{ formula.nomeCor || formula.titulo }}</h1>
        <p><strong>Código:</strong> {{ formula.titulo }}</p>
        <p><strong>Grupo:</strong> {{ formula.grupo }}</p>
        <p v-if="formula.cliente"><strong>Cliente:</strong> {{ formula.cliente }}</p>

        <h2>Componentes:</h2>
        <ul>
          <li v-for="(line, i) in formula.formula" :key="i">
            {{ line.componente }} - {{ line.quantidade }}
          </li>
        </ul>
      </div>
    </div>

    <div v-else>
      <p>Carregando fórmula...</p>
    </div>
  </BaseLayout>
</template>
