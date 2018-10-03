package jsol.algoritms.Arrays;

public class Search {


    /**
     * <h4>Binary search algorithm </h4>
     * <p> Algorithm looks for specified key in given table and returns index of that element.
     * Array must be sorted and can not contains multiple same elements because there is no guarantees which one will be returned.
     * </p>
     *
     * @param array
     * @param key
     * @return<p> Index of element in the array.
     * If there is no such element it will return -1.
     * </p>
     */

    public static int binary(int[] array, int key) {

        int low; // lower index
        int high; // higher index
        int middle;

        low = 0;
        high = array.length - 1;

        while (true) {
            if (array[low] == key) {  // checks if lower index equals to key
                middle = low;
                break;
            }
            if (array[high] == key) {
                middle = high;
                break;
            }
            if (((high - low) <= 1)) {
                // higher index minus lower index can't be less than 1 or if it equals to 1 there is nothing more to search.
                middle = -1;
                break;
            }

            middle = ((high - low) / 2) + low;

            if (array[middle] == key) return middle;

            if (array[middle] < key) low = middle;
            if (array[middle] > key) high = middle;

        }
        return middle;


    }

    public static int linear(int [] table, int target){

        int wanted = -1;
        for (int i = 0; i < table.length; i++) {
            if(table[i]==target){
                wanted = i; // element was found
                break; // stop searching
            }
        }
        return wanted;
    }



}
