﻿using System;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class EnemyMatrix
    {
        public event EventHandler AllEnemiesDestroyed;
        private const int k_DefaultNumberOfRows = 5;
        private const int k_DefaultNumberOfCols = 9;
        private readonly Enemy[,] r_EnemiesMatrix;
        private int m_CurrentNumberOfEnemies;
        private int m_CurrentNumberOfMostRightEnemies;
        private int m_CurrentNumberOfMostLeftEnemies;
        private int m_CurrentNumberOfMostBottomEnemies;

        public EnemyMatrix(GameScreen i_GameScreen)
        {
            m_CurrentNumberOfEnemies = k_DefaultNumberOfRows * k_DefaultNumberOfCols;
            r_EnemiesMatrix = new Enemy[k_DefaultNumberOfRows, k_DefaultNumberOfCols];
            initEnemyMatrix(i_GameScreen);
            markEnemiesClosestToTheBorder(eDirection.RIGHT);
            markEnemiesClosestToTheBorder(eDirection.LEFT);
            markEnemiesClosestToTheBorder(eDirection.BOTTOM);
            AllEnemiesDestroyed += ((Invaders)i_GameScreen.Game).HandleGameOver; // TODO: Fix later
        }

        public enum eDirection
        {
            RIGHT,
            LEFT,
            BOTTOM
        }

        private void initEnemyMatrix(GameScreen i_GameScreen)
        {
            for (int row = 0; row < r_EnemiesMatrix.GetLength(0); row++)
            {
                Enemy.eEnemyType enemyType;

                if (row == 0)
                {
                    enemyType = Enemy.eEnemyType.PINK;
                }
                else if (row < 3)
                {
                    enemyType = Enemy.eEnemyType.BLUE;
                }
                else
                {
                    enemyType = Enemy.eEnemyType.YELLOW;
                }

                for (int col = 0; col < r_EnemiesMatrix.GetLength(1); col++)
                {
                    r_EnemiesMatrix[row, col] = new Enemy(i_GameScreen, enemyType, row, col);
                    r_EnemiesMatrix[row, col].AddActionToTurnEnemies(changeDirection);
                    r_EnemiesMatrix[row, col].AddActionToEnemyDied(enemyDied);
                }
            }
        }

        private void changeDirection()
        {
            foreach (Enemy enemy in r_EnemiesMatrix)
            {
                enemy.ChangeDirection();
            }
        }

        private void enemyDied(bool i_IsMostLeft, bool i_IsMostRight, bool i_IsMostBottom)
        {
            if (i_IsMostLeft)
            {
                m_CurrentNumberOfMostLeftEnemies--;
                if (m_CurrentNumberOfMostLeftEnemies == 0)
                {
                    markEnemiesClosestToTheBorder(eDirection.LEFT);
                }
            }
            if (i_IsMostRight)
            {
                m_CurrentNumberOfMostRightEnemies--;
                if (m_CurrentNumberOfMostRightEnemies == 0)
                {
                    markEnemiesClosestToTheBorder(eDirection.RIGHT);
                }
            }

            if (i_IsMostBottom)
            {
                m_CurrentNumberOfMostBottomEnemies--;
                if (m_CurrentNumberOfMostBottomEnemies == 0)
                {
                    markEnemiesClosestToTheBorder(eDirection.BOTTOM);
                }
            }

            m_CurrentNumberOfEnemies--;
            if (m_CurrentNumberOfEnemies == 0)
            {
                AllEnemiesDestroyed?.Invoke(this, EventArgs.Empty);
            }
        }

        private void markEnemiesClosestToTheBorder(eDirection i_Direction)
        {
            bool isClosestToTheBorder = false;

            switch (i_Direction)
            {
                case eDirection.LEFT:
                    {
                        for (int col = 0; col < r_EnemiesMatrix.GetLength(1); col++)
                        {
                            for (int row = 0; row < r_EnemiesMatrix.GetLength(0); row++)
                            {
                                if (r_EnemiesMatrix[row, col].Visible)
                                {
                                    m_CurrentNumberOfMostLeftEnemies++;
                                    isClosestToTheBorder = true;
                                    r_EnemiesMatrix[row, col].IsMostLeft = true;
                                }
                            }

                            if (isClosestToTheBorder)
                            {
                                break;
                            }
                        }

                        break;
                    }
                case eDirection.RIGHT:
                    {
                        for (int col = r_EnemiesMatrix.GetLength(1) - 1; col >= 0; col--)
                        {
                            for (int row = 0; row < r_EnemiesMatrix.GetLength(0); row++)
                            {
                                if (r_EnemiesMatrix[row, col].Visible)
                                {
                                    m_CurrentNumberOfMostRightEnemies++;
                                    isClosestToTheBorder = true;
                                    r_EnemiesMatrix[row, col].IsMostRight = true;
                                }
                            }

                            if (isClosestToTheBorder)
                            {
                                break;
                            }
                        }

                        break;
                    }
                case eDirection.BOTTOM:
                    {
                        for (int row = r_EnemiesMatrix.GetLength(0) - 1; row >= 0; row--)
                        {
                            for (int col = r_EnemiesMatrix.GetLength(1) - 1; col >= 0; col--)
                            {
                                if (r_EnemiesMatrix[row, col].Visible)
                                {
                                    m_CurrentNumberOfMostBottomEnemies++;
                                    isClosestToTheBorder = true;
                                    r_EnemiesMatrix[row, col].IsMostBottom = true;
                                }
                            }
                            if (isClosestToTheBorder)
                            {
                                break;
                            }
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}