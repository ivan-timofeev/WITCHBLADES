<template>
  <img alt="Vue logo" src="./assets/logo.png">

    <h1 v-if="isLoading">Loading...</h1>
    <h1 v-if="isLoaded">Loaded!</h1>
    <h1 v-if="isLoadingError">Error!</h1>
    <div v-if="isLoaded">
      <p v-for="element in data.elements" :key="element">{{element.albumName}}</p>
    </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'


export default {
  name: 'App',
  data() {
    return {
      isLoading: true,
      isLoaded: false,
      isLoadingError: false,
      data: null
    }
  },
  async mounted() {
    fetch("http://witchblades.com/api/v1/Albums")
      .then((response) => { processRespond(this, response) })
      .catch((reason) => { this.isLoaded = false; })
  }
}
 
async function processRespond(context, response)
{
  let json = await response.json();
  context.isLoaded = true;
  context.data = json;
}

</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
