<script setup>
import { ref } from 'vue'
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
</script>

<template>
  <v-container class="fill-height" fluid>
    <v-row align="center" justify="center">
      <v-col cols="12" sm="6" md="4">
        <v-card>
          <v-card-title class="text-h5">Login</v-card-title>
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
            <v-btn color="primary" @click="login">Entrar</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
