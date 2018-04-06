package jsol.algoritms.Arrays;

public class Sort {
	
	
	public static int [] bubble(int []tab, int arraySize){
	int maxRange = arraySize -1; 
	
	int temp;
	 for (int i = 0; i < maxRange; i++) {
		 for (int j = 0; j < maxRange; j++) {
			 if (tab[j] > tab[j + 1]) {
				/* make swap */
				 temp = tab[j];
				 tab[j] = tab[j + 1];
				 tab[j + 1] = temp;
			 }
		 }
	 }
		
		return tab;
	}

public static int [] bubbleUpgraded(int [] tab, int arraySize){
int maxRange = arraySize -1; 
	
	int temp;
	boolean swapped;
	for (int i = 0; i < maxRange; i++) {
		swapped = false;
		for (int j = 0; j < maxRange; j++) {
			if (tab[j]> tab[j+1]){ 
				
				/* make swap */
				temp = tab[j];
				tab[j] = tab[j+1]; 
				tab[j+1] = temp;
				swapped = true;
			}
			
		}
		if (swapped == false) break;
	}
	
		
		return tab;
	}
public static int [] selection(int [] tab, int arraySize)	{
	int maxRange = arraySize - 1;
	int temp;
	for (int i = 0; i < maxRange; i++) {
		for (int j = i + 1; j <= maxRange; j++) {
			if (tab[i]>tab[j]){
				temp = tab[i];
				tab[i] = tab[j];
				tab[j] = temp;
			}
		}
	}
	
	return tab;
	}

}
 