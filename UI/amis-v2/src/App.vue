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
@import "./assets/css/date-picker.css";

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

::placeholder {
  font-family: MISANotosans-Italic;
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

@keyframes shake {
  0% {
    transform: translate3d(0px, 0, 0);
  }
  25% {
    transform: translate3d(-4px, 0, 0);
  }
  50% {
    transform: translate3d(0px, 0, 0);
  }
  75% {
    transform: translate3d(4px, 0, 0);
  }
  100% {
    transform: translate3d(0px, 0, 0);
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

input[type="checkbox"] {
  position: relative;
  cursor: pointer;
}
input[type="checkbox"]:before {
  content: "";
  display: block;
  position: absolute;
  width: 16px;
  height: 16px;
  top: 0;
  left: 0;
  border: 1px solid #babec5;
  border-radius: 3px;
  background-color: white;
}

input[type="checkbox"]:checked:after {
  content: "";
  display: block;
  width: 5px;
  height: 10px;
  border: solid #08bf1e;
  border-width: 0 2px 2px 0;
  -webkit-transform: rotate(45deg);
  -ms-transform: rotate(45deg);
  transform: rotate(45deg);
  position: absolute;
  /* top: 2px; */
  left: 5px;
}

.unselected {
  user-select: none;
}

.btn {
  height: 36px;
  border: 1px solid #8d9096;
  border-radius: 3px;
  padding: 8px 20px;
  cursor: pointer;
  font-family: MISANotosans-SemiBold;
}

.btn.btn-basic {
  background-color: #ffffff;
}
.btn.btn-basic:hover {
  background-color: #d2d3d6;
}

.btn.btn-add {
  color: #ffffff;
  background-color: #2ca01c;
  border: none;
}

.btn.btn-add:hover {
  background-color: #35bf22;
}
</style>