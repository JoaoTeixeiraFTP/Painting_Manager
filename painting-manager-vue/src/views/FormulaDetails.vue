<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import BaseLayout from '../layouts/BaseLayout.vue'

const route = useRoute()
const formulas = ref([])
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
    <div class="page-container">
      <h1 class="page-title">📘 Detalhes da Fórmula</h1>
      <div v-if="formulas.length">
        <div v-for="(formula, index) in formulas" :key="index" class="formula-card">
          <header class="formula-header">
            <h2 class="formula-title">{{ formula.nomeCor || formula.titulo }}</h2>
          </header>

          <div class="formula-info">
            <p><strong>Código:</strong> {{ formula.titulo }}</p>
            <p><strong>Grupo:</strong> {{ formula.grupo }}</p>
            <p v-if="formula.cliente"><strong>Cliente:</strong> {{ formula.cliente }}</p>
          </div>

          <div class="components">
            <h3>🧪 Componentes</h3>
            <ul>
              <li v-for="(line, i) in formula.formula" :key="i">
                <span class="component-name">{{ line.componente }}</span>
                <span class="component-qty">
                  {{ line.quantidade }} <small>{{ line.unit }}</small>
                </span>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div v-else class="loading">
        <p>Carregando fórmula...</p>
      </div>
    </div>
  </BaseLayout>
</template>

<style scoped>
.page-container {
  max-width: 1000px;
  margin: 0 auto;
}

.page-title {
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 24px;
  text-align: center;
  color: #1f2937;
}

.formula-card {
  background: white;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.08);
  padding: 20px;
  margin-bottom: 24px;
  transition: transform 0.2s, box-shadow 0.2s;
}

.formula-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 14px rgba(0,0,0,0.12);
}

.formula-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12px;
}

.formula-title {
  font-size: 22px;
  font-weight: 600;
  color: #111827;
}

.formula-tag {
  background: #3b82f6;
  color: white;
  font-size: 12px;
  padding: 4px 10px;
  border-radius: 9999px;
  font-weight: 500;
}

.formula-tag.base {
  background: #10b981;
}

.formula-info {
  margin-bottom: 16px;
  color: #374151;
  font-size: 14px;
}

.formula-info p {
  margin: 4px 0;
}

.components h3 {
  font-size: 18px;
  font-weight: 600;
  margin-bottom: 10px;
  color: #1f2937;
}

.components ul {
  list-style: none;
  padding: 0;
}

.components li {
  display: flex;
  justify-content: space-between;
  background: #f9fafb;
  border: 1px solid #e5e7eb;
  border-radius: 8px;
  padding: 10px 14px;
  margin-bottom: 8px;
}

.component-name {
  font-weight: 500;
  color: #111827;
}

.component-qty {
  font-weight: 600;
  color: #2563eb;
}
</style>
