(window.webpackJsonp=window.webpackJsonp||[]).push([[4],{"1GSM":function(t,a,n){"use strict";n.r(a),a.default=""},JJIx:function(t,a,n){"use strict";n.r(a);var e=n("mrSG"),r=n("8Y7J"),s=n("SVse"),c=n("iInd"),d=n("fIz6");let o=class{constructor(){}ngOnInit(){}get orderManagementUrl(){return d.a+"retail-admin/order-management/"}get productManagementUrl(){return d.a+"retail-admin/product-management/"}};o=e.a([Object(r.n)({selector:"app-portal",template:e.b(n("Yg28")).default,styles:[e.b(n("1GSM")).default]})],o);const l=[{path:"",pathMatch:"full",component:o}];let i=class{};i=e.a([Object(r.J)({imports:[c.c.forChild(l)],exports:[c.c]})],i),n.d(a,"PortalModule",(function(){return u}));let u=class{};u=e.a([Object(r.J)({declarations:[o],imports:[s.b,i]})],u)},Yg28:function(t,a,n){"use strict";n.r(a),a.default='<div class="container">\n    <div class="row">\n        <div class="col-md-6 col-sm-12">\n            <div class="card text-center">\n                <div class="card-body">\n                  <h5 class="card-title">Order Management</h5>\n                  <p class="card-text">Handle your orders in a fastest way. Keep your focus only on the orders from your precious customers</p>\n                  <a [href]="orderManagementUrl" class="btn btn-primary">Enter</a>\n                </div>\n            </div>\n        </div>\n        <div class="col-md-6 col-sm-12">\n            <div class="card text-center">\n                <div class="card-body">\n                  <h5 class="card-title">Product Management</h5>\n                  <p class="card-text">Add or update your existing product catalog. Create new catagories with tags and add new product as many as you want</p>\n                  <a [href]="productManagementUrl" class="btn btn-primary">Enter</a>\n                </div>\n            </div>\n        </div>\n    </div>\n</div>\n'}}]);