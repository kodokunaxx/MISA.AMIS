<template>
  <div
    class="
      MISA-popup-add-employee
      dis-flex
      align-items-center
      justify-content-center
    "
  >
    <div class="popup-main">
      <Loading v-if="isLoading"></Loading>
      <div class="popup-header">
        <div class="popup-title p-20 dis-inline-flex">
          <div class="title">Thông tin nhân viên</div>
          <div class="checkbox p-x-20 dis-flex">
            <input
              class="input-checkbox"
              type="checkbox"
              v-model="employeeInfo.IsCustomer"
            />
            <div class="checkbox-content p-l-10">Là khách hàng</div>
          </div>
          <div class="checkbox p-x-20 dis-flex">
            <input
              class="input-checkbox"
              type="checkbox"
              v-model="employeeInfo.IsSupplier"
            />
            <div class="checkbox-content p-l-10">Là nhà cung cấp</div>
          </div>
        </div>
        <div class="popup-close p-12 dis-flex">
          <div class="help-logo logo m-r-6 pointer"></div>
          <div
            class="close-logo logo pointer"
            v-on:click="hidePopupAddEmployee()"
          ></div>
        </div>
      </div>
      <div class="popup-content p-x-32 p-b-20">
        <div class="dis-flex p-b-12">
          <div class="w-1/2 p-r-26">
            <div class="row-input flex-row p-b-12">
              <div class="w-2/5 p-r-6">
                <div class="flex-row">
                  <div class="tooltip-content">Mã</div>
                  <div class="p-l-5" style="color: red">*</div>
                </div>
                <div class="input-text">
                  <input
                    type="text"
                    class="input-required employee-code"
                    ref="autofocus"
                    v-model="employeeInfo.EmployeeCode"
                  />
                </div>
              </div>
              <div class="w-3/5">
                <div class="flex-row">
                  <div class="tooltip-content">Tên</div>
                  <div class="p-l-5" style="color: red">*</div>
                </div>
                <div class="input-text">
                  <input
                    type="text"
                    class="input-required fullname"
                    v-model="employeeInfo.FullName"
                  />
                </div>
              </div>
            </div>
            <div class="row-input p-b-12">
              <div class="flex-row">
                <div class="tooltip-content">Đơn vị</div>
                <div class="p-l-5" style="color: red">*</div>
              </div>
              <div>
                <DxSelectBox
                  class="input-required department-input"
                  :placeholder="''"
                  :search-enabled="true"
                  height="32"
                  item-template="item"
                  display-expr="DepartmentName"
                  value-expr="DepartmentID"
                  :data-source="departmentSource"
                  v-model="employeeInfo.DepartmentId"
                >
                  <template #item="{ data }">
                    <div>
                      <div class="department-code">
                        {{ data.DepartmentCode }}
                      </div>
                      <div class="department-name">
                        {{ data.DepartmentName }}
                      </div>
                    </div>
                  </template>
                </DxSelectBox>
              </div>
            </div>
            <div class="row-input p-b-12">
              <div class="flex-row">
                <div class="tooltip-content">Chức danh</div>
              </div>
              <div class="input-text">
                <input type="text" v-model="employeeInfo.SencondName" />
              </div>
            </div>
          </div>
          <div class="w-1/2">
            <div class="row-input flex-row p-b-12">
              <div class="w-2/5 p-r-12">
                <div class="flex-row">
                  <div class="tooltip-content">Ngày sinh</div>
                </div>
                <div class="input-text">
                  <input type="date" v-model="employeeInfo.DateOfBirth" />
                </div>
              </div>
              <div class="w-3/5">
                <div class="flex-row">
                  <div class="tooltip-content">Giới tính</div>
                </div>
                <div class="input-radio p-y-5">
                  <input
                    type="radio"
                    class="pointer"
                    name="gender-input"
                    id="Male"
                    :checked="employeeInfo.Gender"
                    v-on:click="employeeInfo.Gender = 1"
                  />
                  <label
                    class="m-l-10 m-r-20 pointer"
                    for="Male"
                    v-on:click="employeeInfo.Gender = 1"
                    >Nam</label
                  >
                  <input
                    type="radio"
                    class="pointer"
                    name="gender-input"
                    id="Fmale"
                    :checked="!employeeInfo.Gender"
                    v-on:click="employeeInfo.Gender = 0"
                  />
                  <label
                    class="m-l-10 pointer"
                    for="Fmale"
                    v-on:click="employeeInfo.Gender = 0"
                    >Nữ</label
                  >
                </div>
              </div>
            </div>
            <div class="row-input flex-row p-b-12">
              <div class="w-3/5 p-r-6">
                <div class="flex-row">
                  <div class="tooltip-content">Số CMND</div>
                </div>
                <div class="input-text">
                  <input type="text" v-model="employeeInfo.IdentityNumber" />
                </div>
              </div>
              <div>
                <div class="flex-row">
                  <div class="tooltip-content">Ngày cấp</div>
                </div>
                <div class="input-text">
                  <input type="date" v-model="employeeInfo.IdentityDate" />
                </div>
              </div>
            </div>
            <div class="row-input p-b-12">
              <div class="flex-row">
                <div class="tooltip-content">Nơi cấp</div>
              </div>
              <div class="input-text">
                <input type="text" v-model="employeeInfo.IdentityPlace" />
              </div>
            </div>
          </div>
        </div>
        <div class="flex-col">
          <div class="row-input p-b-12">
            <div class="flex-row">
              <div class="tooltip-content">Địa chỉ</div>
            </div>
            <div class="input-text">
              <input type="text" v-model="employeeInfo.Address" />
            </div>
          </div>
          <div class="row-input p-b-12 dis-flex">
            <div class="w-1/4 p-r-6">
              <div class="flex-row">
                <div class="tooltip-content">ĐT di động</div>
              </div>
              <div class="input-text">
                <input type="text" v-model="employeeInfo.PhoneNumber" />
              </div>
            </div>
            <div class="w-1/4 p-r-6">
              <div class="flex-row">
                <div class="tooltip-content">ĐT cố định</div>
              </div>
              <div class="input-text">
                <input type="text" v-model="employeeInfo.Hotline" />
              </div>
            </div>
            <div class="w-1/4 p-r-6">
              <div class="flex-row">
                <div class="tooltip-content">Email</div>
              </div>
              <div class="input-text">
                <input type="text" v-model="employeeInfo.Email" />
              </div>
            </div>
          </div>
          <div class="row-input p-b-12 dis-flex">
            <div class="w-1/4 p-r-6">
              <div class="flex-row">
                <div class="tooltip-content">Tài khoản ngân hàng</div>
              </div>
              <div class="input-text">
                <input type="text" v-model="employeeInfo.BankAccount" />
              </div>
            </div>
            <div class="w-1/4 p-r-6">
              <div class="flex-row">
                <div class="tooltip-content">Tên ngân hàng</div>
              </div>
              <div class="input-text">
                <input type="text" v-model="employeeInfo.BankName" />
              </div>
            </div>
            <div class="w-1/4 p-r-6">
              <div class="flex-row">
                <div class="tooltip-content">Chi nhánh</div>
              </div>
              <div class="input-text">
                <input type="text" v-model="employeeInfo.BankBranch" />
              </div>
            </div>
          </div>
        </div>

        <div class="footer-container">
          <div class="devide"></div>
          <div class="popup-footer h-36 dis-flex justify-content-between">
            <div class="footer-left">
              <button
                class="btn btn-basic"
                v-on:click="closePopupAddEmployee()"
              >
                Hủy
              </button>
            </div>
            <div class="footer-right">
              <button class="btn btn-basic m-x-10" v-on:click="saveData(null)">
                Cất
              </button>
              <button class="btn btn-add" v-on:click="saveData(1)">
                Cất và Thêm
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <PopupError v-if="isShowErrorPopup" @closePopup="isShowErrorPopup = false">
      {{ errorContent }}
    </PopupError>
    <PopupDuplicate
      v-if="isShowDuplicatePopup"
      @closePopup="isShowDuplicatePopup = false"
      >{{ errorContent }}</PopupDuplicate
    >
    <PopupDataChanged
      v-if="isShowDataChangedPopup"
      @closePopup="isShowDataChangedPopup = false"
      @saveData="saveData(null)"
    ></PopupDataChanged>
  </div>
</template>

<script>
import axios from "axios";
import DxSelectBox from "devextreme-vue/select-box";
import PopupError from "../popup/popup-error.vue";
import PopupDuplicate from "../popup/popup-duplicatecode.vue";
import PopupDataChanged from "../popup/popup-data-changed.vue";
import Loading from "../popup/Loading.vue";
import EventBus from "../../EventBus";

export default {
  props: ["id", "isDuplicate"],
  components: {
    DxSelectBox,
    PopupError,
    PopupDuplicate,
    PopupDataChanged,
    Loading,
  },
  data() {
    return {
      hostApi: "https://localhost:44369/api/v1",
      endpoint: "/employees",
      departmentSource: [],
      employeeInfo: {
        // EmployeeId: null,
        EmployeeCode: "",
        FullName: "",
        DateOfBirth: null,
        Gender: 0,
        SecondName: "",
        IdentityNumber: "",
        IdentityDate: null,
        IdentityPlace: "",
        Address: "",
        PhoneNumber: "",
        Hotline: "",
        Email: "",
        BankAccount: "",
        BankName: "",
        BankBranch: "",
        IsCustomer: 0,
        IsSupplier: 0,
        CreatedBy: "",
        CreatedDate: null,
        ModifiedBy: "",
        ModifiedDate: null,
        DepartmentId: null,
      },
      employeeInfoClone: null, //clone employee để so sánh khi click đóng mà thay đổi data
      cloneOfemployeeInfoClone: null, 
      isShowErrorPopup: false,
      isShowDuplicatePopup: false,
      isShowDataChangedPopup: false,
      errorContent: "",
      enableSubmit: true,
      method: "POST",
      isLoading: true,
    };
  },
  created() {
    // clone employee để so sánh khi click đóng mà thay đổi data (nếu trước đó sử dụng sửa, cất và thêm)
    this.cloneOfemployeeInfoClone = { ...this.employeeInfo };
    this.getDepartments();
    // khi mở popup nếu id null thì sinh mã mới
    if (this.id === null || this.id === undefined) {
      this.bindCode();
    }
  },
  beforeDestroy() {
    localStorage.removeItem("isDuplicate");

    const isChanged = localStorage.getItem("changed");
    if (isChanged === "true") {
      EventBus.$emit("reloadData");
      localStorage.removeItem("changed");
    }
  },
  mounted() {
    this.$refs.autofocus.focus(); //

    if (this.id != null) {
      this.bindData(this.id);
    }
  },
  methods: {
    /** 
     * gán data lên v-model
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
    */
    async bindData(id) {
      const me = this;
      try {
        // lấy data detail về
        const response = await me.getDetail(id);

        // gán data lấy đc vào employeeInfo, đồng nghĩa với việc bind lên form
        this.employeeInfo = response.data.Data;

        // fix ngày
        const dateOfBirth = new Date(me.employeeInfo.DateOfBirth);
        me.employeeInfo.DateOfBirth = me.handleDate(dateOfBirth);

        const identityDate = new Date(me.employeeInfo.IdentityDate);
        me.employeeInfo.IdentityDate = me.handleDate(identityDate);

        // Nếu là nhân bản thì sinh mã mới, fix method thành POST
        const isDuplicate = localStorage.getItem("isDuplicate");
        if (isDuplicate === "true") {
          me.method = "POST"; // fix method

          const response = await me.getNewCode();
          me.employeeInfo.EmployeeCode = response.data.Data;
        }

        // clone data
        me.employeeInfoClone = {
          ...me.employeeInfo,
        };

        // tắt loading
        me.isLoading = false;
      } catch (ex) {
        console.log(ex);
        me.isLoading = false;
      }
    },

    /**
     * Lấy thông tin nhân viên
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    getDetail(id) {
      return axios.get(this.hostApi + this.endpoint + "/" + id);
    },

    /**
     * Xử lý checkbox
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    handleParam() {
      this.employeeInfo.IsCustomer =
        this.employeeInfo?.IsCustomer == true ? 1 : 0;
      this.employeeInfo.IsSupplier =
        this.employeeInfo?.IsSupplier == true ? 1 : 0;
    },

    /**
     * Xử lý ngày tháng
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    handleDate(date) {
      let result = "";
      if (date !== "Invalid Date") {
        let year = date.getFullYear();
        let month = parseInt(date.getMonth()) + 1;
        let day = date.getDate();

        if (month < 10) {
          month = "0" + month;
        }
        if (day < 10) {
          day = "0" + day;
        }
        result = `${year}-${month}-${day}`;
      }

      return result;
    },

    /**
     * Ẩn Popup thêm nhân viên với điều kiện
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    hidePopupAddEmployee() {
      if (
        JSON.stringify(this.employeeInfo) ===
        JSON.stringify(this.employeeInfoClone)
      ) {
        this.$emit("hidePopupAddEmployee");
      } else {
        this.isShowDataChangedPopup = true;
      }
    },

    /**
     * Thêm hoặc update nhân viên
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    saveData(statusSave) {
      this.checkValidate();
      if (!this.enableSubmit) {
        return;
      }
      let apiUrl = "";

      // Nếu là update
      if (this.id != null) {
        apiUrl = this.hostApi + this.endpoint;

        const isDuplicate = localStorage.getItem("isDuplicate");
        if (isDuplicate === "true") {
          this.method = "POST";
        } else {
          this.method = "PUT";
          apiUrl += "/" + this.id;
        }

        // Nếu là insert
      } else {
        apiUrl = this.hostApi + this.endpoint;
      }

      this.handleParam();

      const config = {
        url: apiUrl,
        method: this.method,
        data: this.employeeInfo,
      };
      this.isLoading = true;
      axios
        .request(config)
        .then(() => {
          //nếu là cất thì đóng popup
          if (statusSave === null) {
            this.$emit("hidePopupAddEmployee"); // đóng popup thêm nhân viên
            // this.isLoading = false;
            EventBus.$emit("reloadData"); // get lại data
          }
          // nếu là cất và thêm thì reset form, k đóng popup
          else {
            localStorage.setItem("isDuplicate", "true");
            this.employeeInfo = { ...this.cloneOfemployeeInfoClone };
            this.bindCode();
          }
        })
        .catch((error) => {
          const validateInfo = error.response.data.ValidateInfo;

          // check duplicate
          if (validateInfo && validateInfo.length) {
            validateInfo.forEach((ele) => {
              if (ele.column === "EmployeeCode" && ele.state === "duplicate") {
                this.errorContent = `Mã nhân viên <${this.employeeInfo.EmployeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại`;
                this.isShowDuplicatePopup = true;
              }
            });
          }

          this.isLoading = false;
        });
      // Thêm 1 biến check xem có lưu dữ liệu ko, nó có thì gọi service get data, ko thì thôi để hiệu năng ok
      localStorage.setItem("changed", "true");
    },

    /**
     * Lấy danh sách đơn vị
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    getDepartments() {
      const me = this;
      const endpoint = "/departments";
      axios.get(this.hostApi + endpoint).then((response) => {
        me.departmentSource = response.data.Data;
        me.departmentSource.unshift({
          DepartmentCode: "Mã đơn vị",
          DepartmentName: "Tên đơn vị",
          disabled: true,
        });
      });
    },

    /**
     * Check validate trước khi submit
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    checkValidate() {
      const me = this;
      //const inputRequired = document.querySelectorAll("input.required");

      this.enableSubmit = true;
      // if (inputRequired.length) {
      //   inputRequired.forEach((input) => {
      //     if (input.value.trim() === "") {
      //       input.classList.add("error");
      //       input.focus();
      //       me.isShowErrorPopup = true;
      //       me.enableSubmit = false;
      //       if (input.classList.contains("fullname")) {
      //         me.errorContent = "Tên không được để trống";
      //       } else if (input.classList.contains("employee-code")) {
      //         me.errorContent = "Mã nhân viên không được để trống";
      //       } else {
      //         me.errorContent = "Đơn vị không được để trống";
      //       }
      //     } else {
      //       input.classList.remove("error");
      //     }
      //   });
      // }
      let inputRequi = [
        me.employeeInfo.EmployeeCode,
        me.employeeInfo.FullName,
        me.employeeInfo.DepartmentId,
      ];
      let ipRequired = document.getElementsByClassName("input-required");
      inputRequi.forEach((input, index) => {
        if (input === null || input === "") {
          ipRequired[index].classList.add("error");
          ipRequired[index].focus();
          me.isShowErrorPopup = true;
          me.enableSubmit = false;
          if (index == 0) {
            console.log(input, index);
            me.errorContent = "Mã nhân viên không được để trống";
          } else if (index == 1) {
            me.errorContent = "Tên không được để trống";
          } else if (index == 2) {
            document
              .querySelector(".input-required.department-input input")
              .focus();
            me.errorContent = "Đơn vị không được để trống";
          }
        } else {
          ipRequired[index].classList.remove("error");
        }
      });
    },

    /**
     * Lấy mã nhân viên mới
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    getNewCode() {
      return axios.get(this.hostApi + this.endpoint + "/new-code");
    },

    /**
     * Sinh mã mới và gán vào form
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    async bindCode() {
      const response = await this.getNewCode();
      this.employeeInfo.EmployeeCode = response.data.Data;
      this.employeeInfoClone = {
        ...this.employeeInfo,
      };
      // tắt loading khi lấy đc new code
      this.isLoading = false;
    },

    /**
     * Ẩn popup thêm nhân viên vô điều kiện
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    closePopupAddEmployee() {
      this.$emit("hidePopupAddEmployee");
      EventBus.$emit("reloadData");
    },
  },
};
</script>

<style>
.MISA-popup-add-employee {
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 1500;
}

.popup-main {
  position: relative;
  display: flex;
  width: 900px;
  height: 575px;
  background-color: #fff;
  box-shadow: 0 5px 20px 0 rgb(0 0 0 / 10%);
  flex-direction: column;
}

.popup-header {
  display: flex;
  height: 73px;
  flex-direction: row;
}

.popup-title {
  width: 822px;
  align-items: center;
}

.popup-title .title {
  font-size: 24px;
  font-family: MISANotosans-Bold;
}

.popup-close {
  width: 78px;
}

.popup-content {
  height: calc(100vh - 73px);
}

.tooltip-content {
  font-family: MISANotosans-SemiBold;
}

.MISA-popup-add-employee input.error {
  border-color: red !important;
}

.MISA-popup-add-employee .row-input .input-text > input[type="text"],
input[type="date"] {
  font-family: MISANotosans;
  font-size: 13px;
  width: 100%;
  max-width: 100%;
  height: 32px;
  padding: 6px 10px;
  border: 1px solid #babec5;
  border-radius: 2px;
  box-sizing: border-box;
}

.MISA-popup-add-employee .row-input .input-text > input[type="text"],
input[type="date"]:focus {
  outline-color: #2ca01c;
}

.footer-container {
  height: 90px;
}

.footer-container .devide {
  padding-bottom: 32px;
  margin-bottom: 20px;
  border-bottom: 1px solid #e0e0e0;
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

.input-checkbox {
  width: 18px;
  height: 18px;
}

.dx-list .dx-empty-message,
.dx-list-item-content {
  display: flex !important;
}

.dx-item-content .department-code {
  width: 120px;
}

.dx-item-content .department-name {
  width: 200px;
}

.dx-state-disabled {
  color: black;
  font-weight: bold !important;
  opacity: 1 !important;
}

.dx-texteditor.dx-state-focused {
  border: 1px solid #2ca01c !important;
  border-radius: 2px !important;
}

.dx-texteditor.dx-editor-outlined {
  border: 1px solid #babec5 !important;
  border-radius: 2px !important;
}

.dx-texteditor.dx-editor-outlined.error {
  border: 1px solid red !important;
  border-radius: 2px !important;
}

.dx-list-item.dx-state-focused {
  background-color: #ebedf0 !important;
  color: #35bf22 !important;
}

.dx-list:not(.dx-list-select-decorator-enabled) .dx-list-item.dx-state-active {
  background-color: #ebedf0 !important;
  color: #35bf22 !important;
}

.dx-list-item-selected
  .dx-template-wrapper.dx-item-content.dx-list-item-content {
  color: #35bf22 !important;
}
</style>