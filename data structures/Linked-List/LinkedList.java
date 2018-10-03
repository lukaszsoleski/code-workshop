package jsol.algoritms.dataStructure.myLinkedList;

import java.util.InputMismatchException;
import java.util.Iterator;
import java.util.NoSuchElementException;




/**
 *
 * <h3>double way generic Linked List. </h3>
 * <p> The list is able to store objects that implement the Comparable interface or
 * inherits the class implementing the Comparable interface.
 *  </p>
 */

public class LinkedList<T extends Comparable<? super T>> implements Iterable<T> {
	/**
	 * First element of the list.
	 */
	private Node<T> head;
	/**
		Last element of the list.
	 */
	private Node<T> tail;
	/**
	 * Size of the list.
	 */
	private int size;


	/*
    Default constructor.
     */
	public LinkedList(){
		head = null;
		tail = null;
		size = 0;

	}
	/**
	 * Inserts the given element at the beginning of the list.
	 *
	 * @param value - generic type <i>T extends Comparable<? super T> </i>
	 */
	public boolean insertFirst(T value){
		if (value == null)return false;

		Node<T> temp = new Node<T>(value);

		if (head == null && tail == null){ // if list is empty.
			head = temp;
			tail = temp;
		}
		else {
			temp.setNext(head);
			head.setPrevious(temp);
			head = temp;
		}
		incrementSize();
		return true;

	}
	/**
	 * Appends given element to the end of the list.
	 *@param value - generic type <i>T extends Comparable<? super T> </i>
	 */
	public boolean insertLast(T value){
		if (value == null)return false;
		Node<T> temp = new Node<T>(value);
		if (tail == null && head == null){
			head = temp;
			tail = temp;
		}
		else{
			temp.setPrevious(tail);
			tail.setNext(temp);
			tail = temp;
		}
		incrementSize();
		return true;

	}

	public boolean delete(final T elem){
		if (elem == null)return false;
		if (head == null && tail == null)return false;

		if (elem.compareTo(head.getValue())==0){
			removeFirst();
		}
		else if (elem.compareTo(tail.getValue())==0){
			removeLast();
		}
		else {

			Node<T> current = head;
			Node<T> previous = null;

			while (current.getValue().compareTo(elem) != 0) {
				if (current.getNext() == null) {
					return false;
				}
				previous = current;
				current = current.getNext();

			}

			current = current.getNext();

			current.setPrevious(previous);
			previous.setNext(current);

		}
		decrementSize();
		return true;





	}
	/**
	 *
	 * @param index to delete
	 * @return true if operation was completed otherwise false.
	 */
	public boolean delete(final int index){//


		if (index < 0 || index > size)return false;
		int maxIndex = size - 1;
		if (index == maxIndex){
			removeLast();
		}else if (index ==0){
			removeFirst();
		}else{
			Node<T> current = head;
			Node<T> previous = null;
			int count = 0;
			while (count != index) {
				previous = current;
				current = current.getNext();
				count++;
			}

			current = current.getNext();

			current.setPrevious(previous);
			previous.setNext(current);
		}
		decrementSize();
		return true;
	}
	/**
	 *
	 * @param range - size of new array.
	 * @return array of specified size with elements of list in order from bottom to top.
	 */
	public Object [] toArray(int range){
		if (range < 0 || range > this.size()) throw new IndexOutOfBoundsException();


		Object [] array = new Object[range];
		int count = 0;

		Node<T> current = head;
		while (count <= range){
			array[count] = current.getValue();

			current = current.getNext();

			count ++;
		}
		return array;
	}

	/**
	 *
	 * @param array with specified elements to create new list.
	 * @return custom list with elements in order from bottom to top.
	 */
	public static <E extends Comparable<? super E>> LinkedList<E> asList(E [] array){
		if (array == null) throw new NullPointerException();

		LinkedList<E> list = new LinkedList<>();
		for (int i = 0 ; i < array.length; i++) {
			list.insertLast(array[i]);
		}
		return list;
	}

	/**
	 * Removes first element of the list.
	 */
	public void removeFirst(){
		if (head == null && tail == null ) return;
		if (size == 1){
			head = null;
			tail = null;
		}else {
			head = head.getNext();
			head.setPrevious(null);
		}
		decrementSize();
	}
	public void removeLast(){
		if (tail == null && head == null) return;
		if (size == 1){
			head = null;
			tail = null;

		}else {
			tail = tail.getPrevious();
			tail.setNext(null);

		}
		decrementSize();
	}
	/**
	 * Clears all elements of the list.
	 */
	public void clear(){
		if (size!=0)
		{
			while(size > 0){
				removeFirst();
			}
		}

		System.gc();
	}


	public boolean insertAt(int index, T value){
		if (value == null) throw new NullPointerException("Cannot add null value");

		int half = checkHalf(index);
		if (half == -1) throw new IndexOutOfBoundsException("Insertion is impossible. Index is out of range.");

		Node<T> curr;

		if (half == 0 ){
			if (index == 0) insertFirst(value);
			else {
				curr = head;
				curr = traverseForward(curr, 0, index);
				insertBetween(curr.getPrevious(), curr, value);
			}
		}
		else if(half == 1) {
			if (index == this.size - 1) insertLast(value);
			else{
				curr = tail;
				curr = traverseBackward(curr, this.size - 1, index);
				insertBetween(curr, curr.getNext(), value);
			}
		}else return false;


		return true;
	}

	/**
	 * @param curr - current value
	 * @param start starting point
	 * @param end ending point
	 * @return current node after traversing
	 */
	private Node<T> traverseForward ( Node<T> curr, int start, int end){
		if (!checkIfInRange(start,0,this.size-1))throw new IndexOutOfBoundsException("Traverse forward - index do not exist");
		if (!checkIfInRange(end,0,this.size-1)) throw new IndexOutOfBoundsException("Traverse forward - index do not exist");
		if (end < start) throw new InputMismatchException("The end index can not be smaller than the beginning.");

		if (curr != null)
		{
			int count = start;
			while (end != count){
				curr = curr.getNext();
				count ++;
			}
		}
		return curr;
	}
	private Node<T> traverseBackward(Node<T> curr, int start, int end){
		if (!checkIfInRange(start,0,this.size-1))throw new IndexOutOfBoundsException("Traverse forward - index do not exist");
		if (!checkIfInRange(end,0,this.size-1)) throw new IndexOutOfBoundsException("Traverse forward - index do not exist");
		if (end > start) throw new InputMismatchException("The starting index cannot be smaller than the ending index.");

		if (curr != null)
		{
			int count = start;
			while (end != count){
				curr = curr.getPrevious();
				count --;
			}
		}
		return curr;
	}


	private boolean checkIfInRange(int index,int minimumSize ,int maximumSize){
		return (index >= minimumSize && index<=maximumSize);
	}
	private int checkHalf(int index){
		if (index<0 || index >= size()) return -1;

		int half = this.size/2;
		if (index <= half) return 0;
		else return 1;
	}

	private boolean insertBetween(Node<T> prev, Node<T> cur, T data){
		if (prev != null && cur != null) {
			if (data != null){
				Node<T> temp = new Node<T>(data, cur, prev);
				prev.setNext(temp);
				cur.setPrevious(temp);

			}else return false;
		}else return false;

		incrementSize();
		return true;
	}

	public boolean addOrderedForward(T elem){
		if (elem == null) throw new NullPointerException("Specified element equals null!");

		if (this.head == null) insertFirst(elem);
		else if (head.getValue().compareTo(elem) >= 0 ) insertFirst(elem);
		else if (tail.getValue().compareTo(elem) <= 0) insertLast(elem);
		else {
			Node<T> curr = head;
			while (curr.getValue().compareTo(elem) <= 0) {
					if (curr.getNext() == null) {
						insertLast(elem);
						return true;
					}
					curr = curr.getNext();
			}
			insertBetween(curr.getPrevious(), curr, elem);
		}
 		return true;
	}

	public boolean addOrderedBackwards(T elem){

		if (elem == null) throw new NullPointerException("Given element equals null!");

		if (this.head == null && this.tail == null) insertFirst(elem);
		else if (head.getValue().compareTo(elem) >= 0 ) insertFirst(elem);
		else if (tail.getValue().compareTo(elem) <= 0) insertLast(elem);
		else {
			Node<T> curr = this.tail;
			while (curr.getValue().compareTo(elem) >= 0) {
				if (curr.getPrevious() == null) {
					insertFirst(elem);
					return true;
				}
				curr = curr.getPrevious();
			}
			insertBetween(curr, curr.getNext(), elem);
		}
		return true;
	}

	public int size(){
		return size;
	}
	private void decrementSize(){size --;}
	private void incrementSize(){size ++; }
	private boolean isEmpty(){return size == 0;}

	/**
	 * Iterate forward over the list.
	 * @return List Iterator
	 */
	public ListIterator iterator() {

		return new ListIterator();
	}


	public class ListIterator implements Iterator<T>{
		private Node<T> current;
		private Node<T> previous;

		public ListIterator (){
			current = head;
			previous = null;
		}
		@Override
		public boolean hasNext() {
			if (current!=null && current.getNext() != null)return true;
			else return false;
		}

		@Override
		public T next() {
			if (!hasNext()) throw new NoSuchElementException();
			if ( previous == null )
			{
				previous = current;
				return previous.getValue();
			}
			current = current.getNext();
			return current.getValue();
		}
	}
	public T getValue(int index){

		int half = checkHalf(index);
		Node <T> temp = head;
		if (half == 0) temp = traverseForward(temp,0,index);
		else  if (half == 1) temp = traverseBackward(temp,this.size -1 , index);
		else throw new IndexOutOfBoundsException("Index out of range!");

		return temp.getValue();
	}
	public void printListForward(String command){
		System.out.println(command);
		printListForward();
	}
	public void printListForward(){
		if (isEmpty()) System.out.println("List is empty");
		else {
			Node<T> temp = head;
			int count = 0;
			while (temp != null){
				System.out.print(temp.toString() + " , ");
				count++;
				if (count == 10) {
					System.out.println();
					count = 0;
				}

				temp = temp.getNext();
			}
			System.out.println();
		}
	}
	public void printListBackwards(String command){
		System.out.println(command);
		printListBackwards();
	}
	public void printListBackwards(){
		if (isEmpty()) System.out.println("List is empty");
		else {
			Node<T> temp = tail;
			int count = 0;
			while (temp != null){

				System.out.print(temp.toString() + " , ");
				count++;
				if (count == 10) {
					System.out.println();
					count = 0;
				}

				temp = temp.getPrevious();
			}
			System.out.println();
		}
	}
}

