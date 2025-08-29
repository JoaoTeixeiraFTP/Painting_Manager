<script setup>
import { useRouter } from 'vue-router'

// Recebe a prop formula
const props = defineProps({
  formula: { type: Object, required: true }
})

const router = useRouter()

const openDetail = () => {
  // Usa props.formula, não "formula" diretamente
  router.push(`/formula/${encodeURIComponent(props.formula.titulo)}`)
}
</script>

<template>
  <div class="card" @click="openDetail">
    <p class="nome">{{ props.formula.nomeCor || props.formula.titulo }}</p>
    <p v-if="props.formula.nomeCor" class="codigo">{{ props.formula.titulo }}</p>
    <p class="grupo">{{ props.formula.grupo }}</p>
    <p v-if="props.formula.cliente"><strong>Cliente:</strong> {{ props.formula.cliente }}</p>
  </div>
</template>

<style scoped>
.card {
  background-color: #fff;
  border-radius: 8px;
  padding: 15px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.15);
  cursor: pointer;
  transition: box-shadow 0.3s ease;
}

.card:hover {
  box-shadow: 0 4px 12px rgba(0,0,0,0.25);
}

.nome {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 4px;
}

.codigo {
  font-size: 15px;
  color: #444;
  margin-bottom: 6px;
}

.grupo {
  font-size: 14px;
  color: #555;
  margin-bottom: 5px;
}

.cliente {
  font-size: 14px;
  color: #333;
}
</style>
