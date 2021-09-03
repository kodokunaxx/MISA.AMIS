<template>
  <div>
    <div
      class="MISA-menu-context p-y-2 p-x-1"
      tabindex="0"
      ref="context"
      :style="contextStyle"
      @blur="hideMenuContext()"
    >
      <div
        class="context-item pointer duplicate"
        @click="duplicateData(), hideMenuContext()"
      >
        Nhân bản
      </div>
      <div
        class="context-item pointer delete"
        @click="
          showDeletePopup();
          hideMenuContext();
        "
      >
        Xóa
      </div>
      <div class="context-item pointer stop" @click="hideMenuContext()">
        Ngừng sử dụng
      </div>
    </div>
  </div>
</template>

<script>
import EventBus from "../../EventBus";

export default {
  props: ["top", "left", "id", "code"],
  computed: {
    contextStyle: function () {
      return {
        top: (this.top || -9999) + "px",
        left: (this.left || -9999) + "px",
      };
    },
  },
  data() {
    return {
      isShowMenuContext: true,
    };
  },
  mounted() {
    this.$refs.context.focus();
  },
  methods: {
    /**
     * gửi evet ẩn menucontext cho parent component khi click ra ngoài menucontext
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    hideMenuContext() {
      this.$emit("hideMenuContext");
    },

    /**
     * gửi event showDeletePopup cho App.vue khi click delete
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    showDeletePopup() {
      EventBus.$emit("showDeletePopup", this.id, this.code);
    },

    /**
     * gửi event duplicate cho parent component và set isDuplicate true khi click nhân bản
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    duplicateData() {
      this.$emit("duplicateData");
      localStorage.setItem("isDuplicate", "true");
    },
  },
};
</script>

<style>
.MISA-menu-context {
  position: absolute;
  z-index: 1551;
  border: 1px solid #babec5;
  background-color: #fff;
  transform: translate(-100%, -50%);
  transition: 0.05s;
  outline: none;
}

.context-item {
  padding: 5px 10px;
}

.context-item:hover {
  background-color: #e8e9ec;
  color: #08bf1e;
  transition: 0.2s;
}
</style>