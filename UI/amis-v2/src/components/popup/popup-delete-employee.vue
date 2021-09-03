<template>
  <div
    class="MISA-popup-error dis-flex justify-content-center align-items-center"
  >
    <div class="popup-error-main flex-col p-32">
      <div class="content dis-flex align-items-center">
        <div class="logo-48 warning-logo"></div>
        <div class="error-text p-l-16">
          <slot></slot>
        </div>
      </div>
      <div class="mess-line m-t-16 m-b-20"></div>
      <div class="dis-flex justify-content-between align-items-center">
        <button class="btn btn-basic" @click="closePopup()">Không</button>
        <div class="dis-flex align-items-center">
          <button
            class="btn btn-close p-x-20 p-y-8 m-l-8"
            @click="deleteData()"
          >
            Có
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import EventBus from "../content/Employee.vue";
export default {
  methods: {
    /**
     * gửi event đóng popup cho parent component
     */
    closePopup() {
      this.$emit("showDeletePopup");
    },
    /**
     * gửi event xóa data cho parent component và đóng popup
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    deleteData() {
      //   EventBus.$emit("isLoading");
      this.$emit("deleteData");
      this.closePopup();
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
  z-index: 1550;
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