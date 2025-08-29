import { createRouter, createWebHistory } from "vue-router";
import Login from "../views/Login.vue";
import Dashboard from "../views/Dashboard.vue";
import GroupFormulas from "../views/GroupFormulas.vue";
import FormulaDetails from "../views/FormulaDetails.vue";
import NewFormula from "../views/NewFormula.vue";

const routes = [
  { path: "/login", name: "Login", component: Login },
  { path: "/dashboard", name: "Dashboard", component: Dashboard }, 
  { path: "/group/:id", name: "GroupFormulas", component: GroupFormulas },
  { path: "/formula/:titulo", name: "FormulaDetails", component: FormulaDetails },
  { path: "/new-formula", name: "NewFormula", component: NewFormula },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
