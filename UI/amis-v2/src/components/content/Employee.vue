<template>
  <div class="AMIS-employee">
    <div class="title-distance header-list">
      <div class="title-distance-lelft">Nhân viên</div>
      <div class="title-distance-right">
        <button class="btn btn-add" v-on:click="showPopupAddEmployee()">
          Thêm mới nhân viên
        </button>
      </div>
    </div>
    <div id="layout-dictionary-body">
      <div class="tool">
        <div class="tool-left"></div>
        <div class="tool-right">
          <div class="input-text dis-flex relative">
            <input
              type="text"
              placeholder="Tìm theo mã, tên nhân viên"
              v-on:keyup="search($event.target.value)"
              v-model="searchValue"
            />
            <div class="icon-16 search-icon"></div>
          </div>
          <div
            class="logo refresh-logo m-l-12 pointer"
            @click="reloadData()"
          ></div>

          <div
            class="icon-export-excel w-24 h-24 m-x-12 pointer"
            @click="exportExcel()"
          ></div>

          <div class="icon-setting w-24 h-24 pointer"></div>
        </div>
      </div>
      <table class="table">
        <tr class="table-header dis-inline-flex">
          <th class="table-header-item item-outline-left"></th>
          <th class="table-header-item checkbox-sticky checkbox-width p-x-10">
            <input
              type="checkbox"
              class="w-16 h-16"
              @click="selectAll($event.target.checked)"
            />
          </th>
          <th
            class="table-header-item medium-width p-x-10"
            field="EmployeeCode"
            id="link1"
          >
            MÃ NHÂN VIÊN
          </th>
          <th class="table-header-item long-width p-x-10" field="FullName">
            HỌ VÀ TÊN
          </th>
          <th class="table-header-item very-short-width p-x-10" field="Gender">
            GIỚI TÍNH
          </th>
          <th
            class="table-header-item short-width p-x-10 justify-content-center"
            field="DateOfBirth"
          >
            NGÀY SINH
          </th>
          <th class="table-header-item long-width p-x-10" field="SecondName">
            CHỨC DANH
          </th>
          <th
            class="table-header-item long-width p-x-10"
            field="DepartmentName"
          >
            TÊN ĐƠN VỊ
          </th>
          <th class="table-header-item medium-width p-x-10" field="BankAccount">
            SỐ TÀI KHOẢN
          </th>
          <th class="table-header-item long-width p-x-10" field="BankName">
            TÊN NGÂN HÀNG
          </th>
          <th class="table-header-item long-width p-x-10" field="BankBranch">
            CHI NHÁNH TK NGÂN HÀNG
          </th>
          <th
            class="
              table-header-item
              feature-sticky
              justify-content-center
              medium-width
              p-x-10
            "
          >
            CHỨC NĂNG
          </th>
          <th class="table-header-item item-outline-right"></th>
        </tr>

        <tbody class="table-content">
          <tr
            v-for="employee in employees"
            :key="employee.EmployeeId"
            class="table-content-item dis-inline-flex"
            @dblclick="updateData(employee.EmployeeId)"
          >
            <td class="item-outline-left"></td>
            <td class="content-item checkbox-sticky checkbox-width p-x-10">
              <input type="checkbox" class="w-16 h-16" />
            </td>
            <td class="content-item medium-width p-x-10">
              <div>
                {{ employee.EmployeeCode }}
              </div>
            </td>
            <td class="content-item long-width p-x-10">
              <div>
                {{ employee.FullName }}
              </div>
            </td>
            <td class="content-item very-short-width p-x-10">
              <div>
                {{ employee.Gender | formatGender }}
              </div>
            </td>
            <td class="content-item short-width p-x-10 justify-content-center">
              <div>
                {{ employee.DateOfBirth | formatDate }}
              </div>
            </td>
            <td class="content-item long-width p-x-10">
              <div>
                {{ employee.SecondName }}
              </div>
            </td>
            <td class="content-item long-width p-x-10">
              <div>
                {{ employee.DepartmentName }}
              </div>
            </td>
            <td class="content-item medium-width p-x-10">
              <div>
                {{ employee.BankAccount }}
              </div>
            </td>
            <td class="content-item long-width p-x-10">
              <div>
                {{ employee.BankName }}
              </div>
            </td>
            <td class="content-item long-width p-x-10">
              <div>
                {{ employee.BankBranch }}
              </div>
            </td>
            <td
              class="
                dis-flex
                align-items-center
                content-item
                feature-sticky
                medium-width
              "
            >
              <div
                class="p-x-10 pointer"
                @click="updateData(employee.EmployeeId)"
              >
                Sửa
              </div>
              <div
                class="p-x-10 p-y-8 pointer"
                @click="
                  showMenuContext(
                    $event,
                    employee.EmployeeId,
                    employee.EmployeeCode
                  )
                "
              >
                <div class="icon dropdown-icon"></div>
              </div>
              <div class="dropdown"></div>
            </td>
            <td class="item-outline-right"></td>
          </tr>
        </tbody>
      </table>
      <div
        class="
          no-data
          dis-flex
          align-items-center
          justify-content-center
          m-y-24
        "
        v-if="total === 0"
      >
        <div>
          <img src="../../assets/img/no_data.svg" alt="Không có dữ liệu" />
          <p class="m-t-24 text-center">Không có dữ liệu</p>
        </div>
      </div>
    </div>
    <div class="pagination h-48">
      <div class="paging-left dis-flex">
        Tổng số:
        <span class="m-x-4" style="font-weight: bold">{{ total }}</span> bản ghi
      </div>
      <div class="paging-right dis-flex">
        <DxSelectBox
          :data-source="paginationSource"
          :search-enabled="false"
          :value="paginationSource[1].key"
          display-expr="text"
          value-expr="key"
          width="200"
          height="30"
          item-template="item"
          @value-changed="handleUserSelectPageSize($event)"
        >
          <template #item="{ data }">
            <div class="page-item-custom">
              {{ data.text }}
            </div>
          </template>
        </DxSelectBox>
        <div class="dis-flex m-l-12">
          <div
            class="paging-previous pointer unselected m-r-13"
            :class="pageIndex === 1 ? 'paging-blur' : ''"
            @click="setPageIndex(pageIndex - 1)"
          >
            Trước
          </div>

          <!-- index đầu -->
          <div
            class="pointer paging-index p-x-2"
            :class="1 === pageIndex ? 'paging-index-active' : ''"
            @click="setPageIndex(1)"
          >
            1
          </div>

          <!-- ...  -->
          <div class="etc m-x-6" v-if="pageIndex > 2">...</div>

          <div
            v-for="index in totalIndex"
            :key="index"
            class="pointer"
            @click="setPageIndex(index)"
          >
            <div
              v-if="
                (index !== 1 &&
                  index !== totalIndex &&
                  index <= pageIndex + 2 &&
                  index >= pageIndex) ||
                ((index === totalIndex - 1 || index === totalIndex - 2) &&
                  pageIndex >= totalIndex - 2 &&
                  totalIndex > 3)
              "
            >
              <div v-if="index === 4 && pageIndex === 2"></div>
              <div
                class="m-x-6 paging-index p-x-2"
                :class="index === pageIndex ? 'paging-index-active' : ''"
                v-else
              >
                {{ index }}
              </div>
            </div>
          </div>

          <!-- ...  -->
          <div
            class="etc m-x-6"
            v-if="pageIndex < totalIndex - 2 && totalIndex > 4"
          >
            ...
          </div>

          <!-- index cuối -->
          <div
            class="pointer paging-index p-x-2"
            :class="totalIndex === pageIndex ? 'paging-index-active' : ''"
            @click="setPageIndex(totalIndex)"
            v-if="totalIndex > 1"
          >
            {{ totalIndex }}
          </div>

          <div
            class="paging-next pointer unselected m-l-13"
            @click="setPageIndex(pageIndex + 1)"
            :class="pageIndex === totalIndex ? 'paging-blur' : ''"
          >
            Sau
          </div>
        </div>
      </div>
    </div>

    <MenuContext
      v-if="isShowMenuContext"
      @hideMenuContext="isShowMenuContext = false"
      @duplicateData="showPopupAddEmployee(id)"
      :top="top"
      :left="left"
      :id="id"
      :code="code"
    />

    <Loading v-if="isLoading" />
  </div>
</template>

<script>
import axios from "axios";
import EventBus from "../../EventBus";
import DxSelectBox from "devextreme-vue/select-box";
import MenuContext from "../popup/Menu-Context.vue";
import Loading from "../popup/Loading.vue";

export default {
  components: { Loading, DxSelectBox, MenuContext },
  data() {
    return {
      hostApi: "https://localhost:44369/api/v1",
      endpoint: "/employees/paging",
      employees: [],
      isShowMenuContext: true,
      top: -9999,
      left: -9999,
      id: null,
      code: null,
      paginationSource: [
        { key: 10, text: "10 bản ghi trên 1 trang" },
        { key: 20, text: "20 bản ghi trên 1 trang" },
        { key: 30, text: "30 bản ghi trên 1 trang" },
        { key: 50, text: "50 bản ghi trên 1 trang" },
        { key: 100, text: "100 bản ghi trên 1 trang" },
      ],
      isLoading: true,
      isDuplicate: false,
      timer: null,
      searchValue: "",
      total: 0,
      pageIndex: 1,
      pageSize: 20,
      totalIndex: 1,
    };
  },
  created() {
    const me = this;
    this.getData();

    EventBus.$on("loading", (payload) => {
      me.isLoading = payload;

      // Nếu là false thì delete xong => get lại data
      if (payload === false) {
        me.getData();
      }
    });

    EventBus.$on("reloadData", () => {
      me.reloadData();
    });
  },
  filters: {
    /**
     * Định dạng lại ngày tháng
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */

    formatDate: function (date) {
      if (!date) {
        return "";
      }

      let result = "";
      date = new Date(date);
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
        result = `${day}-${month}-${year}`;
      }

      return result;
    },

    /**
     * Định dạng lại giới tính
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    formatGender(gender) {
      if (gender === null || gender === undefined || gender === "") {
        return "";
      }
      return gender ? "Nam" : "Nữ";
    },
  },
  methods: {
    /**
     * Lấy dữ liệu đổ ra table
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    getData() {
      this.employees = [];
      this.isLoading = true;
      let apiUrl = this.hostApi + this.endpoint;

      if (this.pageSize) {
        apiUrl += `?pageSize=${this.pageSize}`;
        if (Math.ceil(this.total / this.pageSize) >= this.pageIndex) {
          apiUrl += `&pageIndex=${this.pageIndex}`;
        }
      }

      if (this.searchValue.trim() !== "") {
        apiUrl = this.hostApi + "/employees/filter?keyword=" + this.searchValue;
        if (this.pageSize) {
          apiUrl += `&pageSize=${this.pageSize}`;

          if (Math.ceil(this.total / this.pageSize) >= this.pageIndex) {
            apiUrl += `&pageIndex=${this.pageIndex}`;
          }
        }
      }

      axios
        .get(apiUrl)
        .then((response) => {
          this.employees = response.data.Data;
          this.total = response.data.Total;
          this.totalIndex = Math.ceil(this.total / this.pageSize);
          this.isLoading = false;

          // Nếu sau khi get data mà totalIndex < pageIndex, thì set lại page index  = 1
          if (this.totalIndex < this.pageIndex) {
            this.pageIndex = 1;
          }
        })
        .catch((error) => console.log(error));
    },

    /**
     * Hiện popup thêm nhân viên nếu click thêm
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    showPopupAddEmployee(id) {
      EventBus.$emit("showPopup", id);
    },

    /**
     * Hiện popup để sửa nhân viên nếu click sửa
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    updateData(id) {
      this.showPopupAddEmployee(id);
    },
    /**
     *Show menuContext khi click dropdown
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    showMenuContext(event, id, code) {
      console.log(code);
      this.isShowMenuContext = true;
      this.top = event.clientY;
      this.left = event.clientX - 180;
      this.id = id;
      this.code = code;
    },

    /**
     * làm mới data khi click reload
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    reloadData() {
      this.getData();
    },

    /**
     * Tìm kiếm theo keyword
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    search(keyword) {
      const me = this;
      clearTimeout(this.timer);
      this.timer = setTimeout(() => {
        if (keyword.trim() === "") {
          me.getData();
        } else {
          let apiUrl = me.hostApi + "/employees/filter?keyword=" + keyword;
          if (me.pageSize !== -1) {
            apiUrl += `&pageSize=${me.pageSize}`;
          }

          me.isLoading = true;

          axios
            .get(apiUrl)
            .then((response) => {
              me.employees = response.data.Data;
              me.total = response.data.Total;
              me.totalIndex = Math.ceil(me.total / me.pageSize);
              me.pageIndex = 1;
              me.isLoading = false;
            })
            .catch((error) => console.log(error));
        }
      }, 500);
    },

    /**
     * gán page size khi chọn combobox
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    setPageSize(size) {
      this.pageSize = size;
    },

    /**
     * gán pageIndex khi chọn pageIndex
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    setPageIndex(index) {
      if (index > 0 && index <= this.totalIndex) {
        this.pageIndex = index;
        this.getData();
      }
    },

    /**
     * reload data và gán pageSize
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    handleUserSelectPageSize(event) {
      const size = event.value;
      this.setPageSize(size);
      this.getData();
    },

    /**
     * Xuất file excel khi click
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    exportExcel() {
      const url =
        this.hostApi +
        `/employees/export?keyword=${this.searchValue}&pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`;
      window.location.href = url;
    },

    /**
     *
     */
    selectAll(isChecked) {
      const inputs = document.querySelectorAll(
        ".content-item.checkbox-sticky input"
      );

      if (isChecked) {
        for (const input of inputs) {
          input.checked = true;
        }
      } else {
        for (const input of inputs) {
          input.checked = false;
        }
      }
    },
  },
};
</script>

<style>
.AMIS-employee {
  height: 100%;
}
.title-distance {
  height: 102px;
  background-color: #f4f5f8;
  padding: 24px 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.title-distance .title-distance-lelft {
  font-size: 24px;
  font-family: MISANotosans-Bold;
}

#layout-dictionary-body {
  max-width: calc(100vw - 180px);
  max-height: calc(100vh - 198px);
  overflow-y: auto;
  overflow-x: auto;
  background-color: #ffffff;
}

#layout-dictionary-body .tool {
  position: sticky;
  right: 0;
  left: 0;
  height: 72px;
  padding: 16px 16px 10px;
  align-items: center;
  display: flex;
  justify-content: space-between;
}

.tool .tool-right {
  align-items: center;
  display: flex;
}

.tool-right .icon-export-excel {
  background-image: url(../../assets/img/Sprites.64af8f61.svg);
  background-position: -704px -200px;
}

.tool-right .icon-setting {
  background-image: url(../../assets/img/Sprites.64af8f61.svg);
  background-position: -88px -200px;
}

#layout-dictionary-body .table {
  width: 100%;
  /* height: calc(100vh - px);
  overflow: auto; */
}

.table .table-header {
  position: sticky;
  top: 0;
  height: 34px;
  background-color: #eceef1;
  white-space: nowrap;
  text-overflow: ellipsis;
  z-index: 1450;
}

.very-short-width {
  width: 125px;
}

.short-width {
  width: 150px;
}

.medium-width {
  width: 165px;
}

.long-width {
  width: 270px;
}

.checkbox-width {
  width: 40px;
}

.table-header-item {
  display: flex;
  align-items: center;
  overflow: hidden;
  z-index: 1450;
  border-bottom: 1px solid #c7c7c7;
  border-right: 1px solid #c7c7c7;
  font-family: MISANotosans-Bold;
}

.item-outline-left {
  position: sticky;
  top: 0;
  left: 0;
  width: 20px;
  border: none !important;
  z-index: 1400;
  background-color: #fff;
}

.table-header-item.item-outline-left {
  z-index: 1500;
}

.item-outline-right {
  position: sticky;
  top: 0;
  right: 0;
  width: 30px;
  border: none !important;
  z-index: 1400;
  background-color: #fff;
}

.table-header-item.item-outline-right {
  z-index: 1500;
}

.table-content {
  min-height: 100%;
}

.table-content-item:hover .content-item {
  background-color: #f3f8f8 !important;
}

.content-item {
  display: flex;
  align-items: center;
  padding-top: 4px;
  padding-bottom: 4px;
  border-right: 1px dotted #c7c7c7;
  border-bottom: 1px solid #c7c7c7;
  min-height: 47px;
  white-space: wrap;
}

.checkbox-sticky {
  position: sticky;
  top: 0;
  left: 20px;
  z-index: 1400;
  background-color: #fff;
}

.checkbox-sticky > input {
  width: 16px;
  height: 16px;
  cursor: pointer;
}

.checkbox-sticky > input:checked {
  animation: clockwise 0.4s 1;
}

.feature-sticky {
  position: sticky;
  top: 0;
  right: 30px;
  background-color: #fff;
  border-left: 1px dotted #c7c7c7;
  border-right: none !important;
  font-weight: 600;
  color: #0075c0;
}

.table-header-item.feature-sticky {
  background-color: #eceef1;
  border-left: 1px solid #c7c7c7;
  color: black;
}

.table-header-item.checkbox-sticky {
  z-index: 1500;
  background-color: #eceef1;
}

.content-item.feature-sticky {
  justify-content: center;
}

.pagination {
  padding-left: 20px;
  padding-right: 50px;
  background-color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.pagination .paging-right {
  align-items: center;
}

.page-item-custom {
  height: 32px;
}

.tool-right .input-text input {
  font-family: MISANotosans;
  font-size: 13px;
  width: 100%;
  max-width: 100%;
  height: 32px;
  padding: 6px 25px 6px 11px;
  border: 1px solid #babec5;
  border-radius: 2px;
  box-sizing: border-box;
}

.relative {
  position: relative;
}

.input-text .icon-16 {
  cursor: pointer;
  position: absolute;
  right: 8px;
  top: 8px;
}

.tool-right .input-text input:focus {
  outline-color: #2ca01c;
}

.dx-list-item-selected .page-item-custom {
  background-color: #2ca01c;
}

/* .dx-list-item.dx-state-active .page-item-custom {
  background-color: #2ca01c;
} */

.dx-list-item-selected
  .dx-template-wrapper.dx-item-content.dx-list-item-content {
  color: #fff !important;
}

.dx-list:not(.dx-list-select-decorator-enabled)
  .dx-list-item.dx-list-item-selected {
  background-color: #2ca01c !important;
}

.paging-index.paging-index-active {
  padding: 0 6.5px;
  border: 1px solid #e0e0e0;
  font-weight: 700;
}

.paging-right .paging-blur {
  color: #9e9e9e;
}

::-webkit-scrollbar {
  height: 6px; /* height of horizontal scrollbar ← You're missing this */
  width: 4px; /* width of vertical scrollbar */
  border: 1px solid #d5d5d5;
}
</style>