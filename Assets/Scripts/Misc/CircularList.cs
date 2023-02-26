using UnityEngine;

class Node {
    public int data;
    public Node next;

    public Node(int value) {
        data = value;
        next = null;
    }
}

class CircularList {
    private Node head;

    public CircularList() {
        head = null;
    }

    // Method to insert a node with the given value into the list
    public void Insert(int value) {
        Node newNode = new Node(value);
        if (head == null) {
            head = newNode;
            newNode.next = head;
        }
        else {
            Node temp = head;
            while (temp.next != head) {
                temp = temp.next;
            }
            temp.next = newNode;
            newNode.next = head;
        }
    }

    // Method to delete a node with the given value from the list
    public void Delete(int value) {
        if (head == null) {
            Debug.LogError("List is empty.");
            return;
        }
        Node current = head, prev = null;
        while (current.data != value) {
            if (current.next == head) {
                Debug.LogError(value + " not found in the list.");
                return;
            }
            prev = current;
            current = current.next;
        }
        if (current == head && current.next == head) {
            head = null;
            return;
        }
        if (current == head) {
            Node temp = head;
            while (temp.next != head) {
                temp = temp.next;
            }
            head = head.next;
            temp.next = head;
        }
        else if (current.next == head) {
            prev.next = head;
        }
        else {
            prev.next = current.next;
        }
    }

    // Method to find the first node with the given value in the list
    public Node Find(int value) {
        if (head == null) {
            Debug.LogError("List is empty.");
            return null;
        }
        Node current = head;
        while (current.data != value) {
            if (current.next == head) {
                Debug.LogError(value + " not found in the list.");
                return null;
            }
            current = current.next;
        }
        return current;
    }
}
