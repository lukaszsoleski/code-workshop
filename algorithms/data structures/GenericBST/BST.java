package jsol.algoritms.dataStructure.GenericBST;


public class BST <T extends Comparable<T>> {
    private Node root;

    public BST(){
        this.root = null;

    }


    public boolean contains(T value){
        Node current = root;
        while(current!=null){
            if(current.getData().compareTo(value) ==0){
                return true;//if current is greater
            }else if(current.getData().compareTo(value) > 0 ){ //
                current = current.getLeft();
            }else{
                current = current.getRight();
            }
        }
        return false;
    }
    public boolean delete(T id){
        Node parent = root;
        Node current = root;
        boolean isLeftChild = false;
        while(current.getData().compareTo(id)!=0){
            parent = current;
            if(current.getData().compareTo(id)> 0){// traverse in tree
                isLeftChild = true;
                current = current.getLeft();// traverse to left child
            }else{
                isLeftChild = false;
                current = current.getRight(); // traverse to right child
            }
            if(current ==null){
                return false;// value not found, return false value.
            }
        }
        //if i am here that means we have found the node
        //Case 1: if node to be deleted has no children
        if(current.getLeft()==null && current.getRight()==null){
            if(current==root){
                root = null;
            }
            if(isLeftChild ==true){
                parent.setLeft(null);
            }else{
                parent.setRight(null);
            }
        }
        //Case 2 : if node to be deleted has only one child
        else if(current.getRight()==null){// only left child exist.Element to delete has left child.
            if(current==root){
                root = current.getLeft();
            }else if(isLeftChild){ // and it's left child of parent node
                parent.setLeft(current.getLeft()); // node to delete is left child and it also has left child
            }else{// current node is right child
                parent.setRight(current.getLeft());// current node has only left child. Set parent right child.
            }
        }
        else if(current.left==null){// only right child exist.Element to delete has right child.
            if(current==root){
                root = current.right;// if it's root then set it.
            }else if(isLeftChild){// if current element is left child of his parent
                parent.setLeft(current.getRight());
            }else{
                parent.setRight(current.getRight());//current node is right child of his parent
            }
            // Case 3 : if node to be deleted has two children
        }else if(current.left!=null && current.right!=null){

            Node successor	 = getSuccessor(current); //now we have found the minimum element in the right sub tree
            if(current==root){
                root = successor;
            }else if(isLeftChild){
                parent.setLeft(successor);// replace with new sub tree
            }else{
                parent.setRight(successor);
            }
            successor.left = current.left; //keep left sub tree reference
        }
        return true;
    }

    private Node getSuccessor(Node deleteNode){
        Node successor =null;
        Node successorParent =null;
        Node current = deleteNode.getRight();//Successor is the smallest node in the right sub tree of the node to be deleted.
        while(current!=null){
            successorParent = successor;
            successor = current;
            current = current.left;//find smallest value in right sub tree
        }
        //check if successor has the right child, it cannot have left child for sure
        // if it does have the right child, add it to the left of successorParent.
        // successorParent
        if(successor!=deleteNode.right){//
            successorParent.setLeft(successor.getRight()); //swap right child of successor to his parent
            successor.setRight(deleteNode.getRight());// set successor right child as deleteNode right sub tree
        }
        return successor;// replaced element with right child
    }
    public void insert(T id){
        Node<T> newNode = new Node<>(id);
        if(root==null){// tree is empty
            root = newNode; // insert root
            return;// first element inserted
        }
        Node<T> current = root;
        Node<T> parent = null;
        while(true){// traverse tree
            parent = current;
            if(id.compareTo( current.getData()) < 0){ // traverse left sub tree
                current = current.left;
                if(current==null){
                    parent.left = newNode;//
                    return;
                }
            }else{// traverse right sub tree
                current = current.right;
                if(current==null){
                    parent.right = newNode;
                    return;
                }
            }
        }
    }

    public void display(Node root){
      if (root == null)System.out.println("Tree is empty");
      else{
            display(root.left);
            System.out.println(root.data);
            display(root.right);
        }
    }
    public void printPostOrder(){
        if (root == null) System.out.println("Tree is empty");
        else root.printPostOrder();

    }
    public void printInOrder(){
        if (root == null) System.out.println("Tree is empty");
        else root.printInOrder();

    }
    public void printPreOrder(){
        if (root == null) System.out.println("Tree is empty");
        else root.printPreOrder();

    }
    public void clear(){
        if (root != null){
            root.clear();
            root = null;
        }

    }


    public static <T extends Comparable<T>> Node<T> sortedArrayToBST(T[] arr) {
        if (arr.length == 0)
            return null;
        return sortedArrayToBST(arr, 0, arr.length - 1);
    }

    /* A function that constructs Balanced Binary Search Tree
      from a sorted array */
    public static <T extends Comparable<T>> Node sortedArrayToBST(T arr[], int start, int end) {

        /* Base Case */
        if (start > end) {
            return null;
        }

        /* Get the middle element and make it root */
        int mid = (start + end) / 2;
        Node<T> node = new Node<>(arr[mid]);

        /* Recursively construct the left subtree and make it
         left child of root */
        node.left = sortedArrayToBST(arr, start, mid - 1);

        /* Recursively construct the right subtree and make it
         right child of root */
        node.right = sortedArrayToBST(arr, mid + 1, end);

        return node;
    }

    public void setRoot(Node<T> root){
        this.root = root;
    }
}
