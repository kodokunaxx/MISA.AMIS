<template>
  <div id="app">
    <Header />
    <Menu />
    <div class="AMIS-content">
      <router-view></router-view>
    </div>
    <Popup
      v-if="isShowPopup"
      @hidePopupAddEmployee="isShowPopup = false"
      :id="id"
    />
    <PopupDelete
      v-if="isShowDeletePopup"
      @showDeletePopup="isShowDeletePopup = false"
      @deleteData="deleteData()"
      >{{ errorContent }}</PopupDelete
    >
  </div>
</template>

<script>
import axios from "axios";
import EventBus from "./EventBus";
import Header from "./components/layout/Header.vue";
import Menu from "./components/layout/Menu.vue";
import Popup from "./components/popup/popup-add-employee.vue";
import PopupDelete from "./components/popup/popup-delete-employee.vue";

export default {
  name: "App",
  components: {
    Header,
    Menu,
    Popup,
    PopupDelete,
  },
  data() {
    return {
      errorContent: "",
      isShowPopup: false,
      id: null,
      code: null,
      isShowDeletePopup: false,
      hostApi: "https://localhost:44369/api/v1",
      endpoint: "/employees",
    };
  },
  created() {
    const me = this;
    EventBus.$on("showPopup", (id) => {
      me.isShowPopup = true;
      this.id = id;
    });
    EventBus.$on("showDeletePopup", (id, code) => {
      this.errorContent = `Bạn có thực sự muốn xóa Nhân viên <${code}> không?`;
      me.isShowDeletePopup = true;
      me.id = id;
    });
  },
  methods: {
    /**
     * xóa nhân viên
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    deleteData() {
      if (this.id) {
        EventBus.$emit("loading", true);
        axios
          .delete(this.hostApi + this.endpoint + "/" + this.id)
          .then(() => {
            EventBus.$emit("loading", false);
          })
          .catch((error) => console.log(error));
      }
    },
  },
};
</script>

<style>
@import "./assets/css/var-spacing.css";
@import "./assets/css/var-cls.css";
@import "./assets/css/var-other.css";
@import "./assets/css/var-logo.css";
@import "./assets/css/var-font.css";

.AMIS-content {
  position: absolute;
  left: 180px;
  top: 48px;
  width: calc(100vw - 180px);
  height: calc(100vh - 48px);
  overflow-y: auto;
  padding-left: 20px;
  padding-right: 30px;
  background-color: #f8f8f8;
  /* overflow-x: hidden; */
}

@keyframes clockwise {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}
@keyframes anticlockwise {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(-180deg);
  }
}

input[type="radio"] {
  /* remove standard background appearance */
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  /* create custom radio button appearance */
  display: inline-block;
  width: 13px;
  height: 13px;
  padding: 2px;
  /* background-color only for content */
  background-clip: content-box;
  border: 1px solid #bbbbbb;
  background-color: #e7e6e7;
  border-radius: 50%;
}

/* appearance for checked radio button */
input[type="radio"]:checked {
  background-color: #2ca01c;
  border: 1px solid #2ca01c;
}

input[type="radio"] {
  -ms-transform: scale(1.5); /* IE 9 */
  -webkit-transform: scale(1.5); /* Chrome, Safari, Opera */
  transform: scale(1.5);
}
</style>
