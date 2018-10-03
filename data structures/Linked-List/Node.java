package jsol.algoritms.dataStructure.myLinkedList;

/**
 * Generic node class destined for doubly linked list. Node stores T value - generic object, next and previous node of list.
 * @param <T>
 */
class Node <T> {
	/**
	 * data to store in node
	 */
	private T value;
	/**
	 * reference to next node
	 */
	private Node<T> next;
	/**
	 * reference to previous node
	 */
	private Node<T> previous;

	public Node(){
		this.value = null;
		this.next = null;
		this.previous = null;

	}
	/**
	 *
	 * @param value value stored in node.
	 */
	public Node(T value){
		this.value = value;
		this.next = null;
	}
	/**
	 *
	 * @param value value stored in node.
	 * @param next  reference to next node
	 */
	public Node(T value, Node<T> next){
		this.value = value;
		this.next = next;
	}

	/**
	 *
	 * @param value
	 * @param next
	 * @param previous
	 */
	public Node(T value, Node<T> next,Node<T> previous){
		this.value = value;
		this.next = next;
		this.previous = previous;
	}

	/**
	 *
	 * @return Returns stored value.
	 */
	public T getValue() {
		return value;
	}
	/**
	 *
	 * @param value value stored in node.
	 */
	public void setValue(T value) {
		this.value = value;
	}
	/**
	 *
	 * @return reference to next node
	 */
	public Node<T> getNext() {
		return next;
	}
	/**
	 *
	 * @param next reference to next node
	 */
	public void setNext(Node <T> next) {
		this.next = next;
	}

	/**
	 *
	 * @return reference to previous Node
	 */
	public Node<T> getPrevious(){return previous;}

	/**
	 *
	 * @param previous reference to previous node
	 */
	public void setPrevious(Node<T> previous){this.previous = previous;}


	@Override
	public String toString() {
		return value.toString();
	}

}
