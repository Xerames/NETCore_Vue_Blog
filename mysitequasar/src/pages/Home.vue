<template>
  <q-page class="container">
    <Carousel></Carousel>
    <Ministration></Ministration>
  </q-page>
</template>

<script>
import Carousel from "components/Carousel.vue"
import Ministration from "components/Ministration.vue"
export default {
  components: {
    Carousel,
    Ministration
  },
}
</script>
