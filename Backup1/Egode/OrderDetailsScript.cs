using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Egode
{
	// Added by KK on 2016/10/26.
	// Taobao put all order information in a script in the order details page.
	// So, parse the script text to get order information.
	// Like this:
	/*
	<script>
	var data = {"mainOrder":{"operations":[],"statusInfo":{"text":"当前订单状态：商家已发货，等待买家确认","type":"t0"},"totalPrice":[{"type":"line","content":[{"type":"text","value":"694.00"}]},{"type":"line","content":[{"type":"text","value":"满就包邮"}]},{"type":"line","content":[{"type":"text","value":"(快递:0.00"}]},{"type":"line","content":[{"type":"text","value":")"}]}],"columns":["宝贝","宝贝属性","状态","服务","单价","数量","优惠","商品总价"],"extra":{"isShowSellerService":false},"orderInfo":{"lines":[{"type":"line","content":[]},{"type":"line","content":[{"type":"namevalue","value":{"name":"订单编号:","value":[{"type":"text","value":"2610984234481400"}]}},{"type":"namevalue","value":{"name":"支付宝交易号:","value":"2016102521001001740213094950"}},{"type":"namevalue","value":{"name":"创建时间:","value":"2016-10-25 12:04:34"}},{"type":"namevalue","value":{"name":"发货时间:","value":"2016-10-25 16:32:09"}},{"type":"namevalue","value":{"name":"付款时间:","value":"2016-10-25 15:05:44"}}]}],"type":"group"},"id":"2610984234481400","subOrders":[{"priceInfo":"62.00","quantity":"2","service":[],"extra":{"overSold":false,"alicommunOrderDirect":false,"needShowQuantity":0,"xt":false,"needDisplay":false,"payStatus":0,"opWeiQuan":false,"notSupportReturn":false},"tradeStatus":[{"type":"line","content":[{"type":"text","value":"未确认收货"}]}],"id":2610984234491400,"itemInfo":{"skuText":[{"type":"line","content":[]}],"auctionUrl":"//trade.taobao.com/trade/detail/tradeSnap.htm?trade_id=2610984234491400","pic":"//img.alicdn.com/bao/uploaded/i4/TB1TlhjLXXXXXacapXXXXXXXXXX_!!0-item_pic.jpg_sum.jpg","title":"德国代购Verla Zinkletten婴儿童补锌片维生素VC50片覆盆子树莓味","serviceIcons":[{"linkTitle":"保障卡","linkUrl":"http://trade.taobao.com/trade/security/security_card.htm?bizOrderId=2610984234491400","type":3,"url":"//img.alicdn.com/tps/i2/T1S4ysXh8pXXXXXXXX-52-16.png"}]},"promotionInfo":[{"type":"line","content":[{"type":"text","value":"正品代购:省 6.00 元"}]}]},{"priceInfo":"52.00","quantity":"1","service":[],"extra":{"overSold":false,"alicommunOrderDirect":false,"needShowQuantity":0,"xt":false,"needDisplay":false,"payStatus":0,"opWeiQuan":false,"notSupportReturn":false},"tradeStatus":[{"type":"line","content":[{"type":"text","value":"未确认收货"}]}],"id":2610984234501400,"itemInfo":{"skuText":[{"type":"line","content":[]}],"auctionUrl":"//trade.taobao.com/trade/detail/tradeSnap.htm?trade_id=2610984234501400","pic":"//img.alicdn.com/bao/uploaded/i2/TB1Yh5nJVXXXXaKXFXXXXXXXXXX_!!0-item_pic.jpg_sum.jpg","title":"德国代购always卫生巾姨妈巾护垫日用加长5滴水32片284mm装现货","serviceIcons":[{"linkTitle":"保障卡","linkUrl":"http://trade.taobao.com/trade/security/security_card.htm?bizOrderId=2610984234501400","type":3,"url":"//img.alicdn.com/tps/i2/T1S4ysXh8pXXXXXXXX-52-16.png"}]},"promotionInfo":[{"type":"line","content":[{"type":"text","value":"正品代购:省 7.00 元"}]}]},{"priceInfo":"148.00","quantity":"1","service":[],"extra":{"overSold":false,"alicommunOrderDirect":false,"needShowQuantity":0,"xt":false,"needDisplay":false,"payStatus":0,"opWeiQuan":false,"notSupportReturn":false},"tradeStatus":[{"type":"line","content":[{"type":"text","value":"未确认收货"}]}],"id":2610984234511400,"itemInfo":{"skuText":[{"type":"line","content":[]}],"auctionUrl":"//trade.taobao.com/trade/detail/tradeSnap.htm?trade_id=2610984234511400","pic":"//img.alicdn.com/bao/uploaded/i3/TB1Mq59NFXXXXcuXVXXXXXXXXXX_!!0-item_pic.jpg_sum.jpg","title":"德国代购喜宝HiPP有机益生菌奶粉4段1+段1岁12个月600g直邮|现货","serviceIcons":[{"linkTitle":"保障卡","linkUrl":"http://trade.taobao.com/trade/security/security_card.htm?bizOrderId=2610984234511400","type":3,"url":"//img.alicdn.com/tps/i2/T1S4ysXh8pXXXXXXXX-52-16.png"}]},"promotionInfo":[{"type":"line","content":[{"type":"text","value":"正品代购:省 12.00 元"}]}]},{"priceInfo":"136.00","quantity":"3","service":[],"extra":{"overSold":false,"alicommunOrderDirect":false,"needShowQuantity":0,"xt":false,"needDisplay":false,"payStatus":0,"opWeiQuan":false,"notSupportReturn":false},"tradeStatus":[{"type":"line","content":[{"type":"text","value":"未确认收货"}]}],"id":2610984234521400,"itemInfo":{"skuText":[{"type":"line","content":[]}],"auctionUrl":"//trade.taobao.com/trade/detail/tradeSnap.htm?trade_id=2610984234521400","pic":"//img.alicdn.com/bao/uploaded/i3/TB1Ng_nNFXXXXXfXFXXXXXXXXXX_!!0-item_pic.jpg_sum.jpg","title":"德国代购喜宝HiPP有机BIO婴幼儿奶粉4段12+1+岁以上800g直邮|现货","serviceIcons":[{"linkTitle":"保障卡","linkUrl":"http://trade.taobao.com/trade/security/security_card.htm?bizOrderId=2610984234521400","type":3,"url":"//img.alicdn.com/tps/i2/T1S4ysXh8pXXXXXXXX-52-16.png"}]},"promotionInfo":[{"type":"line","content":[{"type":"text","value":"卖家优惠 10.00 元"}]},{"type":"line","content":[{"type":"text","value":"正品代购:省 3.00 元"}]}]}],"payInfo":{"promotions":[{"value":"满就包邮"}],"tmallYfx_bizOrderId":0,"sellerYfx_bizOrderId":0,"showPayDetail":false,"cod":false,"sendPromotions":[],"xiaobao315Yfx_bizOrderId":0,"actualFee":{"name":"实收款","value":"694.00"},"fullPromotion":{"valid":false}},"buyer":{"nick":"shenxumin001","mail":"2***","city":" ","name":"沈旭敏","payToBuyerUrl":"//trade.taobao.com/trade/payToUser.htm?user_type=buyer&biz_order_id=2610984234481400","phoneNum":"15987719529","privateMsgUrl":"//member1.taobao.com/message/addPrivateMsg.htm?recipient_nickname=shenxumin001","id":297780014,"notifyBuyerConfirmUrl":"//trade.taobao.com/trade/sendAlertMail.htm?type=doNotifyBuyerConfirm&biz_order_id=2610984234481400&is_seller=false","guestUser":false,"alipayAccount":"1***"}},"orderBar":{"nodes":[{"index":1,"text":"1. 买家下单"},{"index":2,"text":"2. 买家付款"},{"index":3,"text":"3. 发货"},{"index":4,"text":"4. 买家确认收货"},{"index":5,"text":"5. 评价"}],"currentStepIndex":0,"currentIndex":4},"crumbs":[{"text":"首页","url":"//www.taobao.com"},{"text":"我的淘宝","url":"//i.taobao.com/myTaobao.htm?nekot=1477457647200"},{"text":"已卖出的宝贝","url":"//trade.taobao.com/trade/itemlist/listSoldItems.htm?nekot=1477457647200"}],"operationsGuide":[{"layout":"li","lines":[{"type":"line","content":[{"type":"text","value":"买家(shenxumin001)还有"},{"type":"countdown","value":{"hour":3,"countDown":790681,"day":9,"minute":38,"second":1}},{"type":"text","value":"来完成本次交易的\"确认到货\"。如果期间买家（shenxumin001）没有\"确认到货\"，也没有\"申请退款\"，本交易将自动结束，支付宝将把货款支付给您。"}]},{"type":"line","content":[{"type":"text","value":"如果买家表示未收到货或者收到的货物有问题，请及时联系买家积极处理，友好协商。"}]}],"type":"group"},{"layout":"div","lines":[{"display":"alert","type":"line","content":[{"type":"text","value":"标记信息仅自己可见。若不是本人填写，请小心被骗。"},{"type":"operation","value":{"style":"t16","text":"防骗提醒","type":"operation","url":"http://bbs.taobao.com/catalog/thread/154504-256277274.htm"}}]},{"display":"block","type":"line","content":[{"type":"text","value":"标记："},{"type":"text","value":"＃现货＠微微"}]}],"type":"group"},{"lines":[{"type":"line","content":[]},{"type":"line","content":[{"type":"operation","value":{"dataUrl":"//trade.taobao.com/trade/delayTimeOutDate.htm?bizOrderId=2610984234481400&bizType=200&hasRefund=200","data":{"width":600,"crossOrigin":false,"height":220},"action":"a8","style":"t4","text":"延长收货时间","type":"operation"}},{"type":"operation","value":{"style":"t4","text":"标记","type":"operation","url":"//trade.taobao.com/trade/memo/updateSellMemo.htm?bizOrderId=2610984234481400&sellerId=1029290077&returnUrl=%2F%2Ftrade.taobao.com%2Ftrade%2Fdetail%2FtradeItemDetail.htm%3Fbiz_order_id%3D2610984234481400"}},{"type":"operation","value":{"style":"t4","text":"订单优惠详情","type":"operation","url":"http://smf.taobao.com/index.htm?menu=yhjk&module=yhjk&orderNo=2610984234481400"}}]}],"type":"group"}],"tabs":[{"id":"logistics","title":"收货和物流信息","content":{"alingPhone":"15987719529","logisticsName":"-","nick":"败遍欧洲","address":"沈旭敏，15987719529，云南省 玉溪市 华宁县 盘溪镇 兴文路农业银行生活区 ，652800","logisticsUrl":"//trade.taobao.com/trade/logistics_status.htm?logisType=0&bizOrderId=2610984234481400&bizType=200","shipType":"快递","logisticsNum":"610599516719","newAddress":"沈旭敏，15987719529，云南省 玉溪市 华宁县    盘溪镇 兴文路农业银行生活区，652800"}}],"detailExtra":{"op":false,"b2c":false,"ccc":false,"tradeEnd":false,"outShopOrder":false,"wakeupOrder":false,"refundByTb":false,"success":false,"inRefund":false,"viewed_flag":false}}
	</script>	 
	*/
	public class OrderDetailsScript
	{
		private string _script;
		
		public OrderDetailsScript(string script)
		{
			_script = script;
		}
		
		public bool HasNewAddress
		{
			get
			{
				return !string.IsNullOrEmpty(_script) && _script.ToLower().Contains("newaddress");
			}
		}
		
		public string NewAddress
		{
			get
			{
				if (string.IsNullOrEmpty(_script))
					return string.Empty;
			
				if (!this.HasNewAddress)
					return string.Empty;
				
				/* Modified by KK on 2017/08/18.
				 * Text rules was changed.
				Regex r = new Regex("\"newaddress\":");
				Match m = r.Match(_script);
				if (!m.Success)
					return string.Empty;

				return _script.Substring(m.Index+m.Length+1, _script.IndexOf("\"", m.Index+m.Length+1)-m.Index-m.Length-1)
				*/
				
				// Method 1
				// format:
				// \"newAddress\":\"\u6C88\u71D5\u9752\uFF0C18625070181\uFF0C\u6C5F\u82CF\u7701 \u82CF\u5DDE\u5E02 \u5434\u6C5F\u533A \u677E\u9675\u9547\u592A\u6E56\u5C0F\u533A26\u5E62103\uFF0C215200\"
				string unescapeScript = Regex.Unescape(_script);
				Regex r = new Regex("\"newaddress\":");
				Match m = r.Match(unescapeScript);
				if (!m.Success)
					return string.Empty;

				string newAddr = unescapeScript.Substring(m.Index+m.Length+1, unescapeScript.IndexOf("\"", m.Index+m.Length+1)-m.Index-m.Length-1);
				// <<Method 1

				//// Method 2
				//Regex r = new Regex("新收货地址");
				//Match m = r.Match(_script);
				//if (!m.Success)
				//    return string.Empty;
				
				//// format:
				//// 新收货地址：</span><span class=\"value\" data-reactid=\".0.8.1.1.$1/=1$1.0.0.3.0.0.1\">殷晓倩，13951877761，江苏省 南京市 鼓楼区  凤凰街道   湛江路海棠里东门智能快递柜，210009</span>
				//int spanStart = _script.ToLower().IndexOf("<span ", m.Index);
				//int greaterThan = _script.ToLower().IndexOf(">", spanStart);
				//int spanEnd = _script.ToLower().IndexOf("</span>", spanStart);
				//string newAddr = _script.Substring(greaterThan + 1, spanEnd - greaterThan-1);
				//// <<Method 2.
				
				return newAddr;
			}
		}
	}
}