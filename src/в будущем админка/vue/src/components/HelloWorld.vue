<template>
  <div class="hello">
    <h1>TEST</h1>
    <p>{{arr}}</p>
    <p v-for="item in arr" :key="item">{{item}}</p>
  </div>
</template>

<script>
export default {
  props: ['arr'],
  name: 'HelloWorld',
  data() {
    return [1, 2, 3]
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
