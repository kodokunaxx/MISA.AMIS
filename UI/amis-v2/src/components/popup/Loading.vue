<template>
  <div
    class="
      MISA-loading
      unselected
      dis-flex
      align-items-center
      justify-content-center
    "
  >
    <div class="loading-content">
      <img src="../../assets/img/loading.svg" alt="" />
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
.MISA-loading {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 2000;
}
.MISA-loading .loading-content img {
  animation: clockwise 0.8s infinite linear;
}
</style>