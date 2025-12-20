//====> MARKET SIMULATION
Market test_market = new Market();

test_market.create_limit_order_to_buy(10,30);
test_market.create_limit_order_to_buy(40,10);
test_market.create_limit_order_to_buy(30,1);
test_market.create_limit_order_to_buy(1,30);
test_market.create_limit_order_to_sell(1,50);
test_market.book.show_orderbook();
test_market.complete_one_cycle();



public class Market
{
	DateTime market_time = DateTime.Now;
	int market_ticks = 0;
	public Orderbook book { private set; get;}

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
