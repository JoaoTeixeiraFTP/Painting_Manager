<script setup>
import { ref, onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'
import BaseLayout from '../layouts/BaseLayout.vue'
import FormulaCard from '../components/FormulaCard.vue'

const route = useRoute()
const formulas = ref([])
const groupName = ref('')

const loadFormulas = async (id) => {
  try {
    const response = await fetch(`http://localhost:5029/api/monted/group/${id}`)
    if (!response.ok) throw new Error('Erro ao carregar fórmulas')
    formulas.value = await response.json()

    if (formulas.value.length > 0) {
      groupName.value = formulas.value[0].grupo
    } else {
      groupName.value = 'Sem fórmulas'
    }
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => loadFormulas(route.params.id))

watch(
  () => route.params.id,
  (newId) => {
    loadFormulas(newId)
  }
)
</script>

<template>
  <BaseLayout>
    <div>
      <h1>Fórmulas do grupo: {{ groupName }}</h1>

      <div v-if="formulas.length === 0" class="empty">Nenhuma fórmula encontrada.</div>

      <div v-else class="grid">
        <FormulaCard v-for="(formula, index) in formulas" :key="index" :formula="formula" />
      </div>
    </div>
  </BaseLayout>
</template>

<style scoped>
.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 20px;
  margin-top: 20px;
}

.empty {
  color: #666;
  font-style: italic;
}
</style>
