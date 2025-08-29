import { createRouter, createWebHistory } from 'vue-router';
import Login from "../views/Login.vue";
import Dashboard from "../views/Dashboard.vue";
import GroupFormulas from "../views/GroupFormulas.vue";
import FormulaDetails from "../views/FormulaDetails.vue";
import NewFormula from "../views/NewFormula.vue";
import { useAuthStore } from '../stores/auth'

const routes = [
  { path: "/login", name: "Login", component: Login },
  { path: "/dashboard", name: "Dashboard", component: Dashboard }, 
  { path: "/group/:id", name: "GroupFormulas", component: GroupFormulas },
  { path: "/formula/:titulo", name: "FormulaDetails", component: FormulaDetails },
  { path: "/new-formula", name: "NewFormula", component: NewFormula },
  { path: "/:pathMatch(.*)*", redirect: "/login" } // qualquer rota inválida
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Navigation Guard
router.beforeEach((to, from, next) => {
  const auth = useAuthStore()
  if (to.name !== 'Login' && !auth.token) {
    return next({ name: 'Login' });
  }
  if (to.name === 'Login' && auth.token) {
    return next({ name: 'Dashboard' });
  }
  next();
});

export default router;
