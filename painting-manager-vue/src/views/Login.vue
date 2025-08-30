<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'

const username = ref('')
const password = ref('')
const error = ref('')
const router = useRouter()
const auth = useAuthStore()

const login = async () => {
  error.value = ''
  try {
    await auth.login(username.value, password.value)
    router.push('/dashboard')
  } catch (err) {
    error.value = 'Login inválido!'
    console.error(err)
  }
}

const drops = ref([])

onMounted(() => {
  // Criar drops aleatórios a cada 0.5s
  setInterval(() => {
    if (drops.value.length > 50) drops.value.shift() // limitar quantidade
    const colors = ['#2563eb', '#ef4444', '#f3f4f6', '#10b981']
    drops.value.push({
      id: Date.now() + Math.random(),
      color: colors[Math.floor(Math.random() * colors.length)],
      x: Math.random() * 100,
      y: Math.random() * 100,
      size: 20 + Math.random() * 80 // maior tamanho
    })
  }, 500)
})
</script>

<template>
  <div class="login-container">
    <div class="paint-background">
      <div
        v-for="drop in drops"
        :key="drop.id"
        class="drop"
        :style="{
          left: drop.x + '%',
          top: drop.y + '%',
          width: drop.size + 'px',
          height: drop.size + 'px',
          background: drop.color
        }"
      ></div>
    </div>

    <v-container class="login-form" fluid>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="6" md="4">
          <v-card elevation="12">
            <v-card-title class="text-h5 text-center">Login</v-card-title>
            <v-card-text>
              <v-text-field
                v-model="username"
                label="Utilizador"
                outlined
              ></v-text-field>
              <v-text-field
                v-model="password"
                label="Password"
                type="password"
                outlined
              ></v-text-field>
              <v-alert v-if="error" type="error" dense>{{ error }}</v-alert>
            </v-card-text>
            <v-card-actions>
              <v-btn color="primary" block @click="login">Entrar</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<style scoped>
.login-container {
  position: relative;
  width: 100%;
  height: 100vh;
  overflow: hidden;
  background: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
}

.paint-background {
  position: absolute;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  z-index: 1;
}

.drop {
  position: absolute;
  border-radius: 50%;
  opacity: 0;
  animation: appear 4s ease-in-out forwards; /* mais lento para grandes gotas */
  pointer-events: none;
}

@keyframes appear {
  0% { opacity: 0; transform: scale(0.2); }
  50% { opacity: 0.8; transform: scale(1); }
  100% { opacity: 0; transform: scale(0.8); }
}

.login-form {
  position: relative;
  z-index: 2;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
