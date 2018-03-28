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
	var data = {"mainOrder":{"operations":[],"statusInfo":{"text":"��ǰ����״̬���̼��ѷ������ȴ����ȷ��","type":"t0"},"totalPrice":[{"type":"line","content":[{"type":"text","value":"694.00"}]},{"type":"line","content":[{"type":"text","value":"���Ͱ���"}]},{"type":"line","content":[{"type":"text","value":"(���:0.00"}]},{"type":"line","content":[{"type":"text","value":")"}]}],"columns":["����","��������","״̬","����","����","����","�Ż�","��Ʒ�ܼ�"],"extra":{"isShowSellerService":false},"orderInfo":{"lines":[{"type":"line","content":[]},{"type":"line","content":[{"type":"namevalue","value":{"name":"�������:","value":[{"type":"text","value":"2610984234481400"}]}},{"type":"namevalue","value":{"name":"֧�������׺�:","value":"2016102521001001740213094950"}},{"type":"namevalue","value":{"name":"����ʱ��:","value":"2016-10-25 12:04:34"}},{"type":"namevalue","value":{"name":"����ʱ��:","value":"2016-10-25 16:32:09"}},{"type":"namevalue","value":{"name":"����ʱ��:","value":"2016-10-25 15:05:44"}}]}],"type":"group"},"id":"2610984234481400","subOrders":[{"priceInfo":"62.00","quantity":"2","service":[],"extra":{"overSold":false,"alicommunOrderDirect":false,"needShowQuantity":0,"xt":false,"needDisplay":false,"payStatus":0,"opWeiQuan":false,"notSupportReturn":false},"tradeStatus":[{"type":"line","content":[{"type":"text","value":"δȷ���ջ�"}]}],"id":2610984234491400,"itemInfo":{"skuText":[{"type":"line","content":[]}],"auctionUrl":"//trade.taobao.com/trade/detail/tradeSnap.htm?trade_id=2610984234491400","pic":"//img.alicdn.com/bao/uploaded/i4/TB1TlhjLXXXXXacapXXXXXXXXXX_!!0-item_pic.jpg_sum.jpg","title":"�¹�����Verla ZinklettenӤ��ͯ��пƬά����VC50Ƭ��������ݮζ","serviceIcons":[{"linkTitle":"���Ͽ�","linkUrl":"http://trade.taobao.com/trade/security/security_card.htm?bizOrderId=2610984234491400","type":3,"url":"//img.alicdn.com/tps/i2/T1S4ysXh8pXXXXXXXX-52-16.png"}]},"promotionInfo":[{"type":"line","content":[{"type":"text","value":"��Ʒ����:ʡ 6.00 Ԫ"}]}]},{"priceInfo":"52.00","quantity":"1","service":[],"extra":{"overSold":false,"alicommunOrderDirect":false,"needShowQuantity":0,"xt":false,"needDisplay":false,"payStatus":0,"opWeiQuan":false,"notSupportReturn":false},"tradeStatus":[{"type":"line","content":[{"type":"text","value":"δȷ���ջ�"}]}],"id":2610984234501400,"itemInfo":{"skuText":[{"type":"line","content":[]}],"auctionUrl":"//trade.taobao.com/trade/detail/tradeSnap.htm?trade_id=2610984234501400","pic":"//img.alicdn.com/bao/uploaded/i2/TB1Yh5nJVXXXXaKXFXXXXXXXXXX_!!0-item_pic.jpg_sum.jpg","title":"�¹�����always����������������üӳ�5��ˮ32Ƭ284mmװ�ֻ�","serviceIcons":[{"linkTitle":"���Ͽ�","linkUrl":"http://trade.taobao.com/trade/security/security_card.htm?bizOrderId=2610984234501400","type":3,"url":"//img.alicdn.com/tps/i2/T1S4ysXh8pXXXXXXXX-52-16.png"}]},"promotionInfo":[{"type":"line","content":[{"type":"text","value":"��Ʒ����:ʡ 7.00 Ԫ"}]}]},{"priceInfo":"148.00","quantity":"1","service":[],"extra":{"overSold":false,"alicommunOrderDirect":false,"needShowQuantity":0,"xt":false,"needDisplay":false,"payStatus":0,"opWeiQuan":false,"notSupportReturn":false},"tradeStatus":[{"type":"line","content":[{"type":"text","value":"δȷ���ջ�"}]}],"id":2610984234511400,"itemInfo":{"skuText":[{"type":"line","content":[]}],"auctionUrl":"//trade.taobao.com/trade/detail/tradeSnap.htm?trade_id=2610984234511400","pic":"//img.alicdn.com/bao/uploaded/i3/TB1Mq59NFXXXXcuXVXXXXXXXXXX_!!0-item_pic.jpg_sum.jpg","title":"�¹�����ϲ��HiPP�л��������̷�4��1+��1��12����600gֱ��|�ֻ�","serviceIcons":[{"linkTitle":"���Ͽ�","linkUrl":"http://trade.taobao.com/trade/security/security_card.htm?bizOrderId=2610984234511400","type":3,"url":"//img.alicdn.com/tps/i2/T1S4ysXh8pXXXXXXXX-52-16.png"}]},"promotionInfo":[{"type":"line","content":[{"type":"text","value":"��Ʒ����:ʡ 12.00 Ԫ"}]}]},{"priceInfo":"136.00","quantity":"3","service":[],"extra":{"overSold":false,"alicommunOrderDirect":false,"needShowQuantity":0,"xt":false,"needDisplay":false,"payStatus":0,"opWeiQuan":false,"notSupportReturn":false},"tradeStatus":[{"type":"line","content":[{"type":"text","value":"δȷ���ջ�"}]}],"id":2610984234521400,"itemInfo":{"skuText":[{"type":"line","content":[]}],"auctionUrl":"//trade.taobao.com/trade/detail/tradeSnap.htm?trade_id=2610984234521400","pic":"//img.alicdn.com/bao/uploaded/i3/TB1Ng_nNFXXXXXfXFXXXXXXXXXX_!!0-item_pic.jpg_sum.jpg","title":"�¹�����ϲ��HiPP�л�BIOӤ�׶��̷�4��12+1+������800gֱ��|�ֻ�","serviceIcons":[{"linkTitle":"���Ͽ�","linkUrl":"http://trade.taobao.com/trade/security/security_card.htm?bizOrderId=2610984234521400","type":3,"url":"//img.alicdn.com/tps/i2/T1S4ysXh8pXXXXXXXX-52-16.png"}]},"promotionInfo":[{"type":"line","content":[{"type":"text","value":"�����Ż� 10.00 Ԫ"}]},{"type":"line","content":[{"type":"text","value":"��Ʒ����:ʡ 3.00 Ԫ"}]}]}],"payInfo":{"promotions":[{"value":"���Ͱ���"}],"tmallYfx_bizOrderId":0,"sellerYfx_bizOrderId":0,"showPayDetail":false,"cod":false,"sendPromotions":[],"xiaobao315Yfx_bizOrderId":0,"actualFee":{"name":"ʵ�տ�","value":"694.00"},"fullPromotion":{"valid":false}},"buyer":{"nick":"shenxumin001","mail":"2***","city":" ","name":"������","payToBuyerUrl":"//trade.taobao.com/trade/payToUser.htm?user_type=buyer&biz_order_id=2610984234481400","phoneNum":"15987719529","privateMsgUrl":"//member1.taobao.com/message/addPrivateMsg.htm?recipient_nickname=shenxumin001","id":297780014,"notifyBuyerConfirmUrl":"//trade.taobao.com/trade/sendAlertMail.htm?type=doNotifyBuyerConfirm&biz_order_id=2610984234481400&is_seller=false","guestUser":false,"alipayAccount":"1***"}},"orderBar":{"nodes":[{"index":1,"text":"1. ����µ�"},{"index":2,"text":"2. ��Ҹ���"},{"index":3,"text":"3. ����"},{"index":4,"text":"4. ���ȷ���ջ�"},{"index":5,"text":"5. ����"}],"currentStepIndex":0,"currentIndex":4},"crumbs":[{"text":"��ҳ","url":"//www.taobao.com"},{"text":"�ҵ��Ա�","url":"//i.taobao.com/myTaobao.htm?nekot=1477457647200"},{"text":"�������ı���","url":"//trade.taobao.com/trade/itemlist/listSoldItems.htm?nekot=1477457647200"}],"operationsGuide":[{"layout":"li","lines":[{"type":"line","content":[{"type":"text","value":"���(shenxumin001)����"},{"type":"countdown","value":{"hour":3,"countDown":790681,"day":9,"minute":38,"second":1}},{"type":"text","value":"����ɱ��ν��׵�\"ȷ�ϵ���\"������ڼ���ң�shenxumin001��û��\"ȷ�ϵ���\"��Ҳû��\"�����˿�\"�������׽��Զ�������֧�������ѻ���֧��������"}]},{"type":"line","content":[{"type":"text","value":"�����ұ�ʾδ�յ��������յ��Ļ��������⣬�뼰ʱ��ϵ��һ��������Ѻ�Э�̡�"}]}],"type":"group"},{"layout":"div","lines":[{"display":"alert","type":"line","content":[{"type":"text","value":"�����Ϣ���Լ��ɼ��������Ǳ�����д����С�ı�ƭ��"},{"type":"operation","value":{"style":"t16","text":"��ƭ����","type":"operation","url":"http://bbs.taobao.com/catalog/thread/154504-256277274.htm"}}]},{"display":"block","type":"line","content":[{"type":"text","value":"��ǣ�"},{"type":"text","value":"���ֻ���΢΢"}]}],"type":"group"},{"lines":[{"type":"line","content":[]},{"type":"line","content":[{"type":"operation","value":{"dataUrl":"//trade.taobao.com/trade/delayTimeOutDate.htm?bizOrderId=2610984234481400&bizType=200&hasRefund=200","data":{"width":600,"crossOrigin":false,"height":220},"action":"a8","style":"t4","text":"�ӳ��ջ�ʱ��","type":"operation"}},{"type":"operation","value":{"style":"t4","text":"���","type":"operation","url":"//trade.taobao.com/trade/memo/updateSellMemo.htm?bizOrderId=2610984234481400&sellerId=1029290077&returnUrl=%2F%2Ftrade.taobao.com%2Ftrade%2Fdetail%2FtradeItemDetail.htm%3Fbiz_order_id%3D2610984234481400"}},{"type":"operation","value":{"style":"t4","text":"�����Ż�����","type":"operation","url":"http://smf.taobao.com/index.htm?menu=yhjk&module=yhjk&orderNo=2610984234481400"}}]}],"type":"group"}],"tabs":[{"id":"logistics","title":"�ջ���������Ϣ","content":{"alingPhone":"15987719529","logisticsName":"-","nick":"�ܱ�ŷ��","address":"��������15987719529������ʡ ��Ϫ�� ������ ��Ϫ�� ����·ũҵ���������� ��652800","logisticsUrl":"//trade.taobao.com/trade/logistics_status.htm?logisType=0&bizOrderId=2610984234481400&bizType=200","shipType":"���","logisticsNum":"610599516719","newAddress":"��������15987719529������ʡ ��Ϫ�� ������    ��Ϫ�� ����·ũҵ������������652800"}}],"detailExtra":{"op":false,"b2c":false,"ccc":false,"tradeEnd":false,"outShopOrder":false,"wakeupOrder":false,"refundByTb":false,"success":false,"inRefund":false,"viewed_flag":false}}
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
				//Regex r = new Regex("���ջ���ַ");
				//Match m = r.Match(_script);
				//if (!m.Success)
				//    return string.Empty;
				
				//// format:
				//// ���ջ���ַ��</span><span class=\"value\" data-reactid=\".0.8.1.1.$1/=1$1.0.0.3.0.0.1\">����ٻ��13951877761������ʡ �Ͼ��� ��¥��  ��˽ֵ�   տ��·�����ﶫ�����ܿ�ݹ�210009</span>
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