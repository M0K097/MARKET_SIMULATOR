

Market test_market = new Market();
market_engine test_engine = new market_engine(10,8,100,50,10, test_market);

for (int i = 0; i < 500 ; i++)
{
	test_engine.execute_one_cycle();
	Thread.Sleep(1000);
}


