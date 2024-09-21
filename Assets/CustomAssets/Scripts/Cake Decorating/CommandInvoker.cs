using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Queue<ICommand> m_commandBuffer;
    static List<ICommand> m_commandHistory;

    static int m_commandCounter;

    private void Awake()
    {
        m_commandBuffer = new Queue<ICommand>();
        m_commandHistory = new List<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
        while (m_commandHistory.Count > m_commandCounter)
        {
            m_commandHistory.RemoveAt(m_commandCounter);
        }

        m_commandBuffer.Enqueue(command);
    }

    public static void UndoCommand()
    {
        if (m_commandBuffer.Count <=  0)
        {
            if (m_commandCounter > 0)
            {
                m_commandCounter--;
                m_commandHistory[m_commandCounter].Undo();
            }
        }
    }

    public static bool ContainsItem(GameObject item)
    {
        for (int i = 0; i < m_commandHistory.Count; i++)
        {
            if (m_commandHistory[i].ItemToExecute() == item)
            {
                return true;
            }
        }

        return false;
    }

    public static void UndoItem(GameObject item, bool undoButton)
    {
        if (item == null)
        {
            return;
        }

        for (int i = m_commandHistory.Count - 1; i >= 0; i--)
        {
            if (m_commandHistory[i].ItemToExecute() == item)
            {
                m_commandHistory[i].Undo(undoButton);
                return;
            }
        }
    }

    public static ICommand GetLastCommandOfItem(GameObject item)
    {
        if (item == null)
        {
            return null;
        }

        for (int i = m_commandCounter - 1; i >= 0 && m_commandCounter - 1 < m_commandHistory.Count; i--)
        {
            if (m_commandHistory[i].ItemToExecute() == item)
            {
                return m_commandHistory[i];  
            }
        }

        return null;
    }

    private void Update()
    {
        if (m_commandBuffer.Count > 0)
        {
            ICommand c = m_commandBuffer.Dequeue();
            c.Execute();

            m_commandHistory.Add(c);
            m_commandCounter++;
            Debug.Log("Command History is Length: " + m_commandCounter);
        } 
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (m_commandCounter < m_commandHistory.Count)
                {
                    m_commandHistory[m_commandCounter].Execute();
                    m_commandCounter++;
                }
            }
        }
    }
}
