<template>
  <v-layout align-center justify-center>
    <v-flex xs12 sm8 md6 lg6 x14>
      <v-card>
        <v-toolbar dark color="beu dark-3">
          <v-toolbar-title>
            Acceso al sistema
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field
            v-model="email"
            autofocus
            color="accent"
            label="Email"
            required
          ></v-text-field>
          <v-text-field
            v-model="password"
            type="password"
            color="accent"
            label="Password"
            required
          ></v-text-field>
        </v-card-text>
        <v-flex class="red--text" v-if="error">
          {{ error }}
        </v-flex>
        <v-card-actions class="px-3 pb-3">
          <v-flex text-xs-rignt>
            <v-btn @click="ingresar" color="primary">Ingresar</v-btn>
          </v-flex>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>
<script>
import axios from "axios";

export default {
  data() {
    return {
      email: "",
      password: "",
      error: null,
    };
  },
  methods: {
    ingresar() {
      this.error = null;
      axios
        .post("api/Usuarios/Login", {
          email: this.email,
          password: this.password,
        })
        .then((respuesta) => {
          return respuesta.data;
        })
        .then((data) => {
          this.$store.dispatch("guardarToken", data.token);
          this.$router.push({ name: "home" });
        })
        .catch(err =>  {
          console.log(err.response);
          if (err.response.status == 400) {
            this.error="No es un correo electrónico válido.";
          } else if (err.response.status == 404) {
            this.error="No existe el usuario o datos incorrectos.";
          } else {
            this.error="Ocurrio un error: " + err.response.data;
          }
        });
    },
  },
};
</script>
