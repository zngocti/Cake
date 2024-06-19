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
