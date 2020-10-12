using System;
using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Screens;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class EnemyMatrix : GameComponent
    {
        private event EventHandler AllEnemiesDestroyed;
        private event EventHandler EnemyCausedGameOver;
        private const int k_DefaultNumberOfRows = 5;
        private const int k_DefaultNumberOfCols = 9;
        private const int k_PinkEnemyPoints = 300;
        private const int k_BlueEnemyPoints = 200;
        private const int k_YellowEnemyPoints = 70;
        private const int k_LevelBonusPoints = 100;
        private readonly Enemy[,] r_EnemiesMatrix;
        private readonly int r_BonusPoints;
        private int m_CurrentNumberOfEnemies;
        private int m_CurrentNumberOfMostRightEnemies;
        private int m_CurrentNumberOfMostLeftEnemies;
        private int m_CurrentNumberOfMostBottomEnemies;
        private Enemy m_MostRightEnemy;
        private readonly GameScreen r_GameScreen;

        public EnemyMatrix(GameScreen i_GameScreen) : base (i_GameScreen.Game)
        {
            int addedNumberOfCols;
            if (i_GameScreen.Game.Services.GetService(typeof(IPlayManager)) is IPlayManager playerManager)
            {
                addedNumberOfCols = playerManager.GetEffectiveDifficultyLevel() - 1;
                r_BonusPoints = addedNumberOfCols * k_LevelBonusPoints;
            }
            else
            {
                addedNumberOfCols = 0;
                r_BonusPoints = 0;
            }

            m_CurrentNumberOfEnemies = k_DefaultNumberOfRows * (k_DefaultNumberOfCols + addedNumberOfCols);
            r_EnemiesMatrix = new Enemy[k_DefaultNumberOfRows, (k_DefaultNumberOfCols + addedNumberOfCols)];
            initEnemyMatrix(i_GameScreen);
            markEnemiesClosestToTheBorder(eDirection.RIGHT);
            markEnemiesClosestToTheBorder(eDirection.LEFT);
            markEnemiesClosestToTheBorder(eDirection.BOTTOM);
            r_GameScreen = i_GameScreen;
            i_GameScreen.Game.Window.ClientSizeChanged += sizeChanged;
        }

        public enum eDirection
        {
            RIGHT,
            LEFT,
            BOTTOM
        }

        public void OnAllEnemiesDestroyed(EventHandler i_EventHandler)
        {
            AllEnemiesDestroyed += i_EventHandler;
        }


        public void OnEnemyCausedGameOver(EventHandler i_EventHandler)
        {
            EnemyCausedGameOver += i_EventHandler;
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            if (m_MostRightEnemy.Position.X + m_MostRightEnemy.Width >= r_GameScreen.Game.GraphicsDevice.Viewport.Width)
            {
                float moveBack = m_MostRightEnemy.Position.X + m_MostRightEnemy.Width
                                     - r_GameScreen.Game.GraphicsDevice.Viewport.Width;
                changeEnemiesRightPosition(new Vector2(moveBack, 0));
            }
        }

        private void changeEnemiesRightPosition(Vector2 i_MoveBack)
        {
            foreach(Enemy enemy in r_EnemiesMatrix)
            {
                if (enemy.Enabled)
                {
                    enemy.Position -= i_MoveBack;
                }
            }
        }

        private void initEnemyMatrix(GameScreen i_GameScreen)
        {
            for (int row = 0; row < r_EnemiesMatrix.GetLength(0); row++)
            {
                Enemy.eEnemyType enemyType;
                Color tintColor;
                int enemyPoints;
                if (row == 0)
                {
                    enemyType = Enemy.eEnemyType.PINK;
                    tintColor = Color.LightPink;
                    enemyPoints = k_PinkEnemyPoints;

                }
                else if (row < 3)
                {
                    enemyType = Enemy.eEnemyType.BLUE;
                    tintColor = Color.LightBlue;
                    enemyPoints = k_BlueEnemyPoints;
                }
                else
                {
                    enemyType = Enemy.eEnemyType.YELLOW;
                    tintColor = Color.LightYellow;
                    enemyPoints = k_YellowEnemyPoints;
                }

                enemyPoints += r_BonusPoints;
                for (int col = 0; col < r_EnemiesMatrix.GetLength(1); col++)
                {
                    r_EnemiesMatrix[row, col] = new Enemy(i_GameScreen, enemyType, tintColor, enemyPoints, row, col);
                    r_EnemiesMatrix[row, col].AddOnEnemyWentBelowBorder(EnemyCausedGameOver);
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
                                if (!r_EnemiesMatrix[row, col].Destroyed)
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
                                if (!r_EnemiesMatrix[row, col].Destroyed)
                                {
                                    m_MostRightEnemy = r_EnemiesMatrix[row, col];
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
                                if (!r_EnemiesMatrix[row, col].Destroyed)
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