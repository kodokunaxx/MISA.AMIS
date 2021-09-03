<template>
  <div class="AMIS-dashboard">
    <div class="menu-tab">
      <div class="tab-data dis-flex">
        <div class="tab-item item-active p-t-12 p-b-2">
          <div class="h-38 p-x-20 p-y-14">
            <div class="tab-label">Quy trình</div>
          </div>
        </div>
        <div class="tab-item p-t-12 p-b-2">
          <div class="h-38 p-x-20 p-y-14">
            <div class="tab-label">Thu, chi tiền</div>
          </div>
        </div>
        <div class="tab-item p-t-12 p-b-2">
          <div class="h-38 p-x-20 p-y-14">
            <div class="tab-label">Kiểm kê</div>
          </div>
        </div>
        <div class="tab-item p-t-12 p-b-2">
          <div class="h-38 p-x-20 p-y-14">
            <div class="tab-label">Dự báo dòng tiền</div>
          </div>
        </div>
        <div class="tab-item p-t-12 p-b-2">
          <div class="h-38 p-x-20 p-y-14">
            <div class="tab-label">Báo cáo</div>
          </div>
        </div>
      </div>
    </div>
    <div class="dashboard-content dis-flex align-items-center">
      <div class="content dis-flex justify-content-center">
        <div class="content-left">
          <div class="process-area">
            <div
              class="title dis-flex align-items-center justify-content-center"
            >
              NGHIỆP VỤ TIỀN MẶT
            </div>
            <div class="process-cash">
              <div class="hoverable receive">
                <div
                  class="
                    title-button
                    p-b-10
                    dis-flex
                    align-items-center
                    justify-content-center
                  "
                >
                  Thu tiền
                </div>
              </div>
              <div class="hoverable payment">
                <div
                  class="
                    title-button
                    p-b-10
                    dis-flex
                    align-items-center
                    justify-content-center
                  "
                >
                  Chi tiền
                </div>
              </div>
              <div class="hoverable audit">
                <div
                  class="
                    title-button
                    p-b-10
                    dis-flex
                    align-items-center
                    justify-content-center
                  "
                >
                  Kiểm kê quỹ
                </div>
              </div>
            </div>
          </div>
          <div class="menu-area pointer dis-flex">
            <div
              class="w-1/4 flex-col justify-content-center align-items-center"
            >
              <div class="logo process-customer-logo"></div>
              <div class="label-item">Khách hàng</div>
            </div>
            <div
              class="w-1/4 flex-col justify-content-center align-items-center"
            >
              <div class="logo process-provider-logo"></div>
              <div class="label-item">Nhà cung cấp</div>
            </div>
            <div
              class="w-1/4 flex-col justify-content-center align-items-center"
              @click="redirect()"
            >
              <div class="logo process-employee-logo"></div>
              <div class="label-item">Nhân viên</div>
            </div>
            <div
              class="w-1/4 flex-col justify-content-center align-items-center"
            >
              <div class="logo caculator-logo"></div>
              <div class="label-item">Tính tỷ giá xuất quỹ</div>
            </div>
          </div>
        </div>
        <div class="content-right flex-col m-l-10">
          <div class="title dis-flex align-items-center justify-content-center">
            BÁO CÁO
          </div>
          <div class="report-area p-x-16 pointer">
            <div class="report-item">
              <span class="marker">•</span>
              <div class="report-label">Dòng tiền</div>
            </div>
            <div class="report-item">
              <span class="marker">•</span>
              <div class="report-label">Bảng kê số dư tiền theo ngày</div>
            </div>
            <div class="report-item">
              <span class="marker">•</span>
              <div class="report-label">Sổ kế toán chi tiết quỹ tiền mặt</div>
            </div>
            <div class="report-item">
              <span class="marker">•</span>
              <div class="report-label">S03a2-DN: Sổ nhật ký chi tiền</div>
            </div>
            <div class="report-item non-border">
              <span class="marker">•</span>
              <div class="report-label">S03a1-DN: Sổ nhật ký thu tiền</div>
            </div>
          </div>
          <div
            class="
              footer
              dis-flex
              align-items-center
              justify-content-center
              pointer
            "
          >
            Tất cả báo cáo
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  methods: {
    /**
     * Chuyển hướng đến trang employee khi click
     * CreatedBy: hadm (31/8/2021)
     * ModifiedBy: null
     */
    redirect() {
      this.$router.push("employee").catch((error) => {
        if (error.name != "NavigationDuplicated") {
          throw error;
        }
      });
    },
  },
};
</script>

<style>
.AMIS-dashboard {
  height: 100%;
  background-color: #fff;
}

.menu-tab {
  height: 58px;
  width: 100%;
  background-color: #f4f5f8;
}

.dashboard-content {
  height: calc(100vh - 106px);
  background-color: #f8f8f8;
}

.content {
  width: 100%;
  height: 460px;
}

.tab-item {
  cursor: pointer;
}

.tab-item.item-active {
  height: 58px;
  font-weight: 700;
  border-bottom: 4px solid #2ca01c;
}

.h-38 {
  height: 38px;
}

.content-left {
  width: 688px;
}

.content-right {
  width: 300px;
  background-color: #fff;
  border: 1px solid #e5e5e5;
  border-radius: 3px;
}

.content-left .process-area {
  background-color: #fff;
  border: 1px solid #e5e5e5;
  border-radius: 3px;
}

.title {
  height: 40px;
  font-weight: 700;
  border-bottom: 1px solid #e5e5e5;
  background-color: #ffffff;
}

.content-left .process-cash {
  height: 340px;
  background-image: url("../../assets/img/dash_board.svg");
  position: relative;
  background-color: #ffffff;
}

.hoverable {
  position: absolute;
  padding-top: 60px;
  height: 87.6px;
  width: 90px;
  cursor: pointer;
}

.hoverable.receive {
  top: 40px;
  left: 90px;
}

.hoverable.payment {
  top: 215px;
  left: 90px;
}

.hoverable.audit {
  top: 125px;
  left: 335px;
  width: 125px !important;
}

.content-left .menu-area {
  background-color: #fff;
  border: 1px solid #e5e5e5;
  border-radius: 3px;
  margin-top: 10px;
  height: 70px;
}

.content-right .report-item {
  display: flex;
  height: 74px;
  border-bottom: 1px solid #e5e5e5;
  padding: 16px 0;
  align-items: center;
}

.report-area {
  border-bottom: 1px solid #e5e5e5;
}

.marker {
  font-size: 24px;
  margin-right: 10px;
}

.non-border {
  border: none !important;
}

.content-right .footer {
  height: 44px;
  color: #0075c0;
}
</style>