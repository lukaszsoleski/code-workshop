package jsol.algoritms.Arrays;



public class ThreadedBinarySearch implements Runnable {

	
	private final int [] table;
	private int key;
	private int lowerIndex;
	private int higherIndex;
	private int pivot;
	

	private boolean found;
	private volatile boolean carryOn;
	/**
	 *  <h3> ThreadedBinarySearch </h3>
	 *  <p> Algorithm looks for specified key in given table with use of <i><q>Threads</q> </i>.
	 *  Constructor requires setting lower and higher index of an area where one of threads will be looking for specified value.
	 *  As a default lower index equals 0 and higher index equals specified table length.
	 *  If specified table contains multiple particular elements than it's not ensure which element will be found.
	 *	Constructor of the class uses Builder Pattern. Example:
	 *</p>
	 *	<pre>
	 *  <i> new ThreadedBinarySearch.Builder(final Table [])
	 *  	.key() 
	 *  	.lowerIndex() 
	 *  	.higherIndex() 
	 *  	.build()
	 *  </i>
	 *  </pre>
	 *  <p>
	 *	
	 *  When one of threads will find specified value then other threads will stop searching as well.
	 *  <br>
	 *  To check if the index was found use following method: <i>getIndexFound() </i> <br>
	 *  <ul>
	 *  	<li> If value was found returns index. </li>
	 *  	<li> if value wasn't found returns <i>-1</i>. </li>
	 *  </ul>
	 *  <br>
	 *  </p>
	 *  
	 *  
	 */
	private ThreadedBinarySearch(Builder builder){ // private constructor for builder class
		table = builder.table;
		key = builder.key;
		lowerIndex = builder.lowerIndex;
		higherIndex= builder.higherIndex;
		
		found = false;
		carryOn = true;
	
	}
	@Override
	public void run() {
		
		while ((lowerIndex<=higherIndex) && (!found)){
			if(carryOn){ // value shared to all threads.
				// If one of the threads sets this value to false, all will end their work
				pivot = ((higherIndex - lowerIndex)/2)+lowerIndex; // set pivot element
				if(table[pivot]==key){ // If pivot value equals to key end work. Element was found.
					found = true; // prevents further working
					carryOn = false; // sets a variable that controls thread work.
				}
				if(!found) 
				{
					if(table[pivot]< key) lowerIndex = pivot +1;
					else if (table[pivot]> key) higherIndex = pivot -1;
				}
			}
		}
		
	}
	/**
	 * <h4> Get Index Found </h4>
	 * @return <p> Index of specified value. If the value wasn't found returns <i>-1</i>. </p>
	 * 
	 */
	public int getIndexFound() {
		if(found == true) return pivot; // element was found
		else return -1;
	}

	public static class Builder{ // Simple static Builder class which follow Builder Pattern.
		private final int [] table;
		private int key; //  looking for this value in table given above.  
		private int lowerIndex;
		private int higherIndex;
		
		public Builder(int [] table){
			this.table = table;
		
			lowerIndex = 0; // as default
			higherIndex = table.length;
		}
		
	
		public Builder key(final int key){
			this.key = key;
			return this;
		}
		public Builder lowerIndex(final int lowerIndex){
			this.lowerIndex = lowerIndex;
			return this;
		}
		public Builder higherIndex(final int higherIndex){
			this.higherIndex = higherIndex;
			return this;
		}
		
		public ThreadedBinarySearch build(){
			return new ThreadedBinarySearch(this);
		}
	}
}
