package jsol.algoritms.dataStructure.GenericBST;


 class Node<T extends Comparable<T>> {

    protected T data;
    protected Node left;
    protected Node right;

    public Node(T data) {
        this.data = data;
        left = right = null;
    }

    public T getData() {
        return data;
    }

    public void setData(T data) {
        this.data = data;
    }

    public Node getLeft() {
        return left;
    }

    public void setLeft(Node left) {
        this.left = left;
    }

    public Node getRight() {
        return right;
    }

    public void setRight(Node right) {
        this.right = right;
    }
    /**
     * Prints tree in pre-order.
     *
     *
     *In this traversal method, the root node is visited first, then the left subtree and finally the right subtree.
     *
     * Until all nodes are traversed −
     Step 1 − Visit root node.
     Step 2 − Recursively traverse left subtree.
     Step 3 − Recursively traverse right subtree.
     */
    public void printPreOrder()
    {
        System.out.println(this.data);
        if(left != null) left.printPreOrder();
        if(right != null) right.printPreOrder();
    }


    /**
     * Prints tree in in-order
     *
     *
     * In this traversal method, the left subtree is visited first, then the root and later the right sub-tree.
     * We should always remember that every node may represent a subtree itself.

     If a binary tree is traversed in-order, the output will produce sorted key values in an ascending order.

     Until all nodes are traversed −
     Step 1 − Recursively traverse left subtree.
     Step 2 − Visit root node.
     Step 3 − Recursively traverse right subtree.

     */
    public void printInOrder()
    {
        if(left != null) left.printInOrder();
        System.out.println(this.data);
        if(right != null) right.printInOrder();
    }


    /**
     * Prints tree in post-order
     *
     * In this traversal method, the root node is visited last, hence the name.
     * First we traverse the left subtree, then the right subtree and finally the root node.
     *
     * Until all nodes are traversed −
     Step 1 − Recursively traverse left subtree.
     Step 2 − Recursively traverse right subtree.
     Step 3 − Visit root node.
     *
     */
    public void printPostOrder()
    {
        if(left != null) left.printPostOrder();
        if(right != null) right.printPostOrder();

        System.out.println(this.data);
    }

    /**
     * Deletes tree in post-order traversal method.
     */
    public void clear()
    {
        if(left != null) left.clear();
        if(right != null) right.clear();

        left = null;
        right = null;
    }

}
