<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import BaseLayout from '../layouts/BaseLayout.vue'

const router = useRouter()

// Estado do formulário
const form = ref({
  color: {
    groupName: '',
    code: '',
    name: ''
  },
  isBase: true,         // manter boolean
  clientName: '',
  formula: [{ componentName: '', quantity: 0 }]
})

// Listas carregadas da API
const groups = ref([])
const colors = ref([])

// Carregar grupos e cores existentes
onMounted(async () => {
  try {
    const gRes = await fetch('http://localhost:5029/api/groups/list')
    const gData = await gRes.json()
    groups.value = gData
    console.log('[NewFormula] Groups carregados:', gData)

    const cRes = await fetch('http://localhost:5029/api/colors/list')
    const cData = await cRes.json()
    colors.value = cData
    console.log('[NewFormula] Colors carregadas:', cData)
  } catch (err) {
    console.error('[NewFormula] Erro ao carregar dados iniciais:', err)
  }
})

// Manipulação de linhas
const addLine = () => {
  form.value.formula.push({ componentName: '', quantity: 0 })
}

const removeLine = (index) => {
  form.value.formula.splice(index, 1)
}

// Submissão com logs
const submitForm = async () => {
  try {
    // Converter para payload “limpo”
    const payload = {
      color: {
        groupName: form.value.color.groupName?.trim(),
        code: form.value.color.code?.trim(),
        name: form.value.color.name?.trim()
      },
      // garantir boolean (se vier string por algum motivo)
      isBase: form.value.isBase === true || form.value.isBase === 'true',
      clientName: form.value.clientName?.trim() || null,
      // limpar linhas vazias e converter quantity -> número
      formula: form.value.formula
        .map(l => ({
          componentName: (l.componentName ?? '').trim(),
          quantity: Number(l.quantity)
        }))
        .filter(l => l.componentName.length > 0 && !Number.isNaN(l.quantity) && l.quantity > 0)
    }

    console.log('[NewFormula] Payload a enviar:', payload)

    const response = await fetch('http://localhost:5029/api/monted/create', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    })

    if (!response.ok) {
      const text = await response.text().catch(() => '(sem corpo)')
      console.error(`[NewFormula] Falhou: ${response.status} ${response.statusText} — corpo:`, text)
      throw new Error(`Erro ao criar fórmula (${response.status})`)
    }

    const created = await response.json().catch(() => null)
    console.log('[NewFormula] Sucesso! Resposta da API:', created)

    alert('✅ Fórmula criada com sucesso!')
    router.push('/dashboard')
  } catch (error) {
    console.error('[NewFormula] Exceção ao criar fórmula:', error)
    alert('❌ Ocorreu um erro ao criar a fórmula. Ver consola.')
  }
}
</script>

<template>
  <BaseLayout>
    <div class="page-container">
      <h1 class="title">➕ Adicionar Nova Fórmula</h1>

      <form @submit.prevent="submitForm" class="form-container">
        <!-- CARD COR -->
        <div class="card">
          <h2>🎨 Dados da Cor</h2>

          <label>
            Grupo:
            <select v-model="form.color.groupName">
              <option disabled value="">-- Selecione ou escreva --</option>
              <option v-for="g in groups" :key="g.id" :value="g.name">{{ g.name }}</option>
            </select>
            <!-- Mostra input apenas quando o valor não corresponde a nenhum grupo existente -->
            <input
              v-if="form.color.groupName && !groups.some(g => g.name === form.color.groupName)"
              v-model="form.color.groupName"
              placeholder="Escreva novo grupo"
            />
          </label>

          <label>
            Código:
            <input v-model="form.color.code" required />
          </label>

          <label>
            Nome da Cor:
            <input
              v-model="form.color.name"
              list="color-names"
              placeholder="Escreva ou escolha"
              required
            />
            <datalist id="color-names">
              <option v-for="c in colors" :key="c.id" :value="c.name" />
            </datalist>
          </label>
        </div>

        <!-- CARD FORMULA BASE OU CLIENTE -->
        <div class="card">
          <h2>⚙️ Tipo de Fórmula</h2>
          <label>
            <!-- usar :value para garantir boolean -->
            <input type="radio" :value="true" v-model="form.isBase" /> Base
          </label>
          <label>
            <input type="radio" :value="false" v-model="form.isBase" /> Cliente
          </label>

          <div v-if="form.isBase === false">
            <label>
              Nome do Cliente:
              <input v-model="form.clientName" placeholder="Obrigatório quando Cliente" />
            </label>
          </div>
        </div>

        <!-- CARD COMPONENTES -->
        <div class="card">
          <h2>🧪 Componentes</h2>
          <div v-for="(line, index) in form.formula" :key="index" class="formula-line">
            <input
              v-model="line.componentName"
              placeholder="Nome do componente"
              required
            />
            <input
              v-model.number="line.quantity"
              type="number"
              step="0.01"
              min="0.01"
              placeholder="Quantidade"
              required
            />
            <button
              type="button"
              class="remove-btn"
              @click="removeLine(index)"
              v-if="form.formula.length > 1"
            >
              ✖
            </button>
          </div>
          <button type="button" class="add-btn" @click="addLine">➕ Adicionar Componente</button>
        </div>

        <button type="submit" class="submit-btn">💾 Salvar Fórmula</button>
      </form>
    </div>
  </BaseLayout>
</template>

<style scoped>
.page-container { max-width: 900px; margin: 0 auto; }
.title { font-size: 28px; font-weight: bold; margin-bottom: 24px; text-align: center; color: #1f2937; }
.form-container { display: flex; flex-direction: column; gap: 24px; }
.card { background: white; padding: 20px; border-radius: 12px; box-shadow: 0 2px 6px rgba(0,0,0,0.1); }
.card h2 { font-size: 20px; margin-bottom: 16px; color: #111827; }
label { display: block; margin-bottom: 12px; font-weight: 500; color: #374151; }
input, select { display: block; width: 100%; margin-top: 4px; padding: 10px; border: 1px solid #d1d5db; border-radius: 6px; font-size: 14px; }
.formula-line { display: flex; gap: 12px; margin-bottom: 10px; }
.remove-btn { background: #ef4444; border: none; color: white; border-radius: 6px; padding: 6px 10px; cursor: pointer; }
.remove-btn:hover { background: #f87171; }
.add-btn { background: #3b82f6; border: none; color: white; border-radius: 6px; padding: 8px 14px; cursor: pointer; font-weight: bold; }
.add-btn:hover { background: #60a5fa; }
.submit-btn { background: #10b981; border: none; color: white; border-radius: 8px; padding: 12px; font-weight: bold; font-size: 16px; cursor: pointer; transition: background 0.2s; }
.submit-btn:hover { background: #34d399; }
</style>
