def canTrigger$HARBOUR_NORMAL$TradeQuest_$YIELD$_DONE(argsList):
	
	# Read Parameters 1+2 from the two events and check if enough yield is stored in city
	eEvent = gc.getInfoTypeForString("EVENT_$HARBOUR_UPPERCASE$_TRADE_QUEST_$YIELD$_DONE_1")
	event = gc.getEventInfo(eEvent)
	iYieldID = event.getGenericParameter(2)
	iQuantity = event.getGenericParameter(1) # for Quest Done this should be e.g. 1000

	# Now we call the Generic Helper Function
	bTrigger = CanDo$HARBOUR_NORMAL$Trade(argsList, iYieldID, iQuantity)
	
	return bTrigger
