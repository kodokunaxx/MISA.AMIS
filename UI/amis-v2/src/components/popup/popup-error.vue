<template>
  <div
    class="MISA-popup-error dis-flex justify-content-center align-items-center"
  >
    <div class="popup-error-main flex-col p-32">
      <div class="content dis-flex align-items-center">
        <div class="logo-48 error-logo"></div>
        <div class="error-text p-l-16">
          <slot></slot>
        </div>
      </div>
      <div class="mess-line m-t-16 m-b-20"></div>
      <div class="dis-flex justify-content-center align-items-center">
        <button class="btn btn-close p-x-20 p-y-8" @click="closePopup()">
          Đóng
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  methods: {
    /**
     * gửi event đóng popup cho parent component
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    closePopup() {
      this.$emit("closePopup");
    },
  },
};
</script>

<style>
.MISA-popup-error {
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 1551;
}

.MISA-popup-error .popup-error-main {
  width: 444px;
  height: 182px;
  background-color: #fff;
}

.mess-line {
  border-bottom: 1px solid #b8bcc3;
}

.btn.btn-close {
  color: #fff;
  border: none;
  background-color: #2ca01c;
}

.btn.btn-close:hover {
  background-color: #35bf22;
}
</style>