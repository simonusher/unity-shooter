using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    [SerializeField] private string databaseFilePath = "";
    public PlayerData loggedInPlayer;

    private void Awake()
    {
        if(manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        } else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    public void Register(string login, string password)
    {
        bool playerRegistered = TryRegister(login, password);
        if (playerRegistered)
        {
            SceneManager.LoadScene("Login");
        }
    }

    private bool TryRegister(string login, string password)
    {
        IDbConnection connection = GetDbConnection();
        IDbCommand command = connection.CreateCommand();
        connection.Open();
        IDbTransaction transaction = connection.BeginTransaction();
        command.Transaction = transaction;

        try
        {
            string registerQuery = string.Format("INSERT INTO Users(login, password) Values ('{0}', '{1}')", login, password);
            command.CommandText = registerQuery;
            command.ExecuteNonQuery();
            string userQuery = string.Format("SELECT * FROM Users WHERE login = '{0}'", login);
            command.CommandText = userQuery;
            IDataReader reader = command.ExecuteReader();
            int userId = -1;
            while (reader.Read())
            {
                userId = int.Parse(reader["userId"].ToString());
            }
            reader.Close();
            for (int i = 0; i < 6; i++)
            {
                string userTargetCommand = string.Format("INSERT INTO UserTargets(userId, targetId, isShotDown) VALUES({0}, {1}, {2})", userId, i, 0);
                command.CommandText = userTargetCommand;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
            return true;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            return false;
        }
        finally
        {
            command.Dispose();
            connection.Close();
        }
    }

    public void LogIn(string login, string password)
    {
        TryLogIn(login, password);
        if (loggedInPlayer != null)
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void TryLogIn(string login, string password)
    {
        IDbConnection connection = GetDbConnection();
        IDbCommand command = connection.CreateCommand();
        connection.Open();
        string loginQuery = string.Format("SELECT * FROM Users WHERE login = '{0}' AND password = '{1}'", login, password);
        command.CommandText = loginQuery;
        IDataReader reader = command.ExecuteReader();
        PlayerData playerData = null;
        while(reader.Read())
        {
            int userId = int.Parse(reader["userId"].ToString());
            bool[] targetsTakenDown = readTargetsTakenDown(userId, connection);
            playerData = new PlayerData(userId, login, targetsTakenDown);
        }
        loggedInPlayer = playerData;
        reader.Close();
        command.Dispose();
        connection.Close();
    }

    private bool[] readTargetsTakenDown(int playerId, IDbConnection connection)
    {
        string sqlRead = string.Format("SELECT * FROM UserTargets WHERE userId = {0}", playerId);
        IDbCommand command = connection.CreateCommand();
        command.CommandText = sqlRead;
        IDataReader reader = command.ExecuteReader();
        List<bool> targetsTakenDown = new List<bool>();
        while(reader.Read())
        {
            int isShotDown = int.Parse(reader["isShotDown"].ToString());
            targetsTakenDown.Add(isShotDown == 1);
        }
        command.Dispose();
        reader.Close();
        return targetsTakenDown.ToArray();
    }

    private IDbConnection GetDbConnection()
    {
        string connectionString = "URI=file:" + Application.dataPath + databaseFilePath;
        IDbConnection connection = new SqliteConnection(connectionString);
        return connection;
    }

    public void SetObjectShotDown(int index)
    {
        if(loggedInPlayer != null)
        {
            loggedInPlayer.ObjectsShotDown[index] = true;
        }
    }

    public void ResetObjectsShotDown()
    {
        for(int i = 0; i < loggedInPlayer.ObjectsShotDown.Length; i++)
        {
            loggedInPlayer.ObjectsShotDown[i] = false;
        }
    }

    public void SaveState()
    {
        IDbConnection connection = GetDbConnection();
        IDbCommand command = connection.CreateCommand();
        connection.Open();
        IDbTransaction transaction = connection.BeginTransaction();
        command.Transaction = transaction;

        try
        {
            for (int i = 0; i < 6; i++)
            {
                int shotDown = loggedInPlayer.ObjectsShotDown[i] ? 1 : 0;
                int userId = loggedInPlayer.UserId;
                string userTargetCommand = string.Format("UPDATE UserTargets SET isShotDown = {0} WHERE userId = {1} AND targetId = {2}", shotDown, userId, i);
                command.CommandText = userTargetCommand;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
        }
        finally
        {
            command.Dispose();
            connection.Close();
        }
    }

    private void Logout()
    {
        loggedInPlayer = null;
    }

    public void SaveStateAndExit()
    {
        SaveState();
        Logout();
        SceneManager.LoadScene("Login");
    }
}
