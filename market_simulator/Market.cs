public class Market
{
	DateTime market_time = DateTime.Now;
	public Orderbook book { private set; get;}

	public void create_market_order(order_type type, decimal price)
	{
		market_time = DateTime.Now;
		Order market_order = new Order(000,type,price,1,market_time);
		book.execute_market_order(market_order);
	}	

	public void create_limit_order_to_buy(decimal price, double amount)
	{
		market_time = DateTime.Now;
		Console.WriteLine("creating limit oder to buy");
		book.create_order(order_type.ask, price, amount, market_time);

	}
	public void create_limit_order_to_sell(decimal price, double amount)
	{
		market_time = DateTime.Now;
		Console.WriteLine("creating limit oder to sell");
		book.create_order(order_type.bid, price, amount, market_time);

	}

	public void complete_one_cycle()
	{
		book.match_orders();
		book.show_orderbook();
	}


	public Market()
	{
		book = new Orderbook();
	}

}
