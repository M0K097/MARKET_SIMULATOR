
Market test_market = new Market();
market_engine test_engine = new market_engine(20,5,100,50,10, test_market);



for (int i = 0; i < 500 ; i++)
{
	test_engine.create_random_orders();
	test_market.complete_one_cycle();
	Thread.Sleep(1000);
}


