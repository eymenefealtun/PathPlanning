using PathFinding;

namespace PathPlanning.Utilities
{
    public class Tools
    {
        MainForm _mainform;

        public Tools(MainForm mainform)
        {
            _mainform = mainform;
        }

        public Tools()
        {

        }

        public void Refresh()
        {

            _mainform._moveSoldierX = 0;
            _mainform._moveSoldierY = 0;
            _mainform._currentSoldierNumber = 0;
            for (int i = 0; i < _mainform._numberOfSoldiers; i++)
            {
                _mainform._allSoldiers[i].Dispose();
            }
            for (int i = 0; i < _mainform._numberOfTowers; i++)
            {
                _mainform._allTowers[i].Dispose();
            }
            for (int i = 0; i < _mainform._numberOfArcher; i++)
            {
                _mainform._allArchers[i].Dispose();
            }

            _mainform.panelSoldiersEye.Controls.Clear();
            _mainform.panelSoldiersEye.Controls.Add(_mainform.panelTargetSoldiersEye);
            _mainform.panelSoldiersEye.Controls.Add(_mainform.soldierEyePictureBox);

            _mainform.mainTimer.Stop();
            _mainform.panelTarget.Visible = false;
            _mainform.panelTargetSoldiersEye.Visible = false;
            _mainform.panelGodView.Invalidate(); //removes circles
            _mainform._currentHealth = _mainform._health;
            _mainform.lblCurrentHealth.Text = String.Empty;
            //_allArchers = new PictureBox[];
            _mainform._knownTowerNumber = 0;
            _mainform._knownArcherNumber = 0;
            _mainform._knownObstacleNumber = 0;
            _mainform._soldierStuckNumber = 0;

        }

        public void SetFirstLocation()
        {
            try
            {
                int[] scnenarioUserVariables = new int[14];

                _mainform._mainPanelSize = _mainform.tboxPanelSize.Text == null || _mainform.tboxPanelSize.Text == string.Empty ? _mainform._mainPanelSize = 700 : _mainform._mainPanelSize = Convert.ToInt32(_mainform.tboxPanelSize.Text);
                scnenarioUserVariables[0] = _mainform._mainPanelSize;
                _mainform._archerSightRange = _mainform.tboxArcherSight.Text == null || _mainform.tboxArcherSight.Text == string.Empty ? _mainform._archerSightRange = 70 : _mainform._archerSightRange = Convert.ToInt32(_mainform.tboxArcherSight.Text);
                scnenarioUserVariables[1] = _mainform._archerSightRange;
                _mainform._archerHitRange = _mainform.tboxHitRangeOfArcher.Text == null || _mainform.tboxHitRangeOfArcher.Text == string.Empty ? _mainform._archerHitRange = 40 : _mainform._archerHitRange = Convert.ToInt32(_mainform.tboxHitRangeOfArcher.Text);
                scnenarioUserVariables[2] = _mainform._archerHitRange;
                int defaultStep = _mainform._mainPanelSize / 70;
                _mainform._defaultStep = defaultStep;
                if (_mainform._mainPanelSize >= 300 && _mainform._mainPanelSize <= 700 && _mainform._archerSightRange >= _mainform._archerHitRange)
                {
                    Graphics graphicsGodPanel = _mainform.panelGodView.CreateGraphics();

                    _mainform.panelGodView.Height = _mainform._mainPanelSize;
                    _mainform.panelGodView.Width = _mainform._mainPanelSize;

                   _mainform.RefreshGame();
                   _mainform.panelTarget.Visible = true;            
                   _mainform.panelTargetSoldiersEye.Visible = true;
                    _mainform.soldierEyePictureBox.Visible = true;


                    #region userVariables
                    _mainform._soldierMaxMovement = _mainform.tboxSoldierMaxMovement.Text == null || _mainform.tboxSoldierMaxMovement.Text == string.Empty ? _mainform._soldierMaxMovement = _mainform._defaultStep : _mainform._soldierMaxMovement = Convert.ToInt32(_mainform.tboxSoldierMaxMovement.Text);
                    scnenarioUserVariables[3] = _mainform._soldierMaxMovement;

                    _mainform._archerMaxMovement = _mainform.tboxMaxMovementOfArcher.Text == null || _mainform.tboxMaxMovementOfArcher.Text == string.Empty ? _mainform._archerMaxMovement = _mainform._defaultStep : _mainform._archerMaxMovement = Convert.ToInt32(_mainform.tboxMaxMovementOfArcher.Text);
                    scnenarioUserVariables[4] = _mainform._archerMaxMovement;

                    _mainform._numberOfSoldiers = _mainform.tboxNumberOfSoldiers.Text == null || _mainform.tboxNumberOfSoldiers.Text == string.Empty ? _mainform._numberOfSoldiers = 1 : _mainform._numberOfSoldiers = Convert.ToInt32(_mainform.tboxNumberOfSoldiers.Text);
                    scnenarioUserVariables[5] = _mainform._numberOfSoldiers;

                    _mainform._numberOfTowers = _mainform.tboxNumberOfTowers.Text == null || _mainform.tboxNumberOfTowers.Text == string.Empty ? _mainform._numberOfTowers = 1 : _mainform._numberOfTowers = Convert.ToInt32(_mainform.tboxNumberOfTowers.Text);
                    scnenarioUserVariables[6] = _mainform._numberOfTowers;

                    _mainform._towerSight = _mainform.tboxSightRangeOfTowers.Text == null || _mainform.tboxSightRangeOfTowers.Text == string.Empty ? _mainform._towerSight = 35 : _mainform._towerSight = Convert.ToInt32(_mainform.tboxSightRangeOfTowers.Text);
                    scnenarioUserVariables[7] = _mainform._towerSight;

                    _mainform._soldierSight = _mainform.tboxSightOfSoldier.Text == null || _mainform.tboxSightOfSoldier.Text == string.Empty ? _mainform._soldierSight = 35 : _mainform._soldierSight = Convert.ToInt32(_mainform.tboxSightOfSoldier.Text);
                    scnenarioUserVariables[8] = _mainform._soldierSight;

                    _mainform._health = _mainform.tboxHealthOfSoldier.Text == null || _mainform.tboxHealthOfSoldier.Text == string.Empty ? _mainform._health = 100 : _mainform._health = Convert.ToInt32(_mainform.tboxHealthOfSoldier.Text);
                    scnenarioUserVariables[9] = _mainform._health;

                    _mainform._towerHitPower = _mainform.tboxHitPowerOfTower.Text == null || _mainform.tboxHitPowerOfTower.Text == string.Empty ? _mainform._towerHitPower = 1 : _mainform._towerHitPower = Convert.ToInt32(_mainform.tboxHitPowerOfTower.Text);
                    scnenarioUserVariables[10] = _mainform._towerHitPower;

                    _mainform._minDistanceFromTower = _mainform.tboxMinDistanceFromTarget.Text == null || _mainform.tboxMinDistanceFromTarget.Text == string.Empty ? _mainform._minDistanceFromTower = 0 : _mainform._minDistanceFromTower = Convert.ToInt32(_mainform.tboxMinDistanceFromTarget.Text);
                    scnenarioUserVariables[11] = _mainform._minDistanceFromTower;

                    _mainform._numberOfArcher = _mainform.tboxNumberOfArchers.Text == null || _mainform.tboxNumberOfArchers.Text == string.Empty ? _mainform._numberOfArcher = 1 : _mainform._numberOfArcher = Convert.ToInt32(_mainform.tboxNumberOfArchers.Text);
                    scnenarioUserVariables[12] = _mainform._numberOfArcher;

                    _mainform._archerHitPower = _mainform.tboxHitPowerOfArcher.Text == null || _mainform.tboxHitPowerOfArcher.Text == string.Empty ? _mainform._archerHitPower = 1 : _mainform._archerHitPower = Convert.ToInt32(_mainform.tboxHitPowerOfArcher.Text);
                    scnenarioUserVariables[13] = _mainform._archerHitPower;
                    _mainform._scenarioUserVariables = scnenarioUserVariables;

                    _mainform.tboxPanelSize.Text = _mainform._mainPanelSize.ToString();
                    _mainform.tboxArcherSight.Text = _mainform._archerSightRange.ToString();
                    _mainform.tboxHitRangeOfArcher.Text = _mainform._archerHitRange.ToString();
                    _mainform.tboxSoldierMaxMovement.Text = _mainform._soldierMaxMovement.ToString();
                    _mainform.tboxMaxMovementOfArcher.Text = _mainform._archerMaxMovement.ToString();
                    _mainform.tboxNumberOfSoldiers.Text = _mainform._numberOfSoldiers.ToString();
                    _mainform.tboxNumberOfTowers.Text = _mainform._numberOfTowers.ToString();
                    _mainform.tboxSightRangeOfTowers.Text = _mainform._towerSight.ToString();
                    _mainform.tboxSightOfSoldier.Text = _mainform._soldierSight.ToString();
                    _mainform.tboxHealthOfSoldier.Text = _mainform._health.ToString();
                    _mainform.tboxHitPowerOfTower.Text = _mainform._towerHitPower.ToString();
                    _mainform.tboxMinDistanceFromTarget.Text = _mainform._minDistanceFromTower.ToString();
                    _mainform.tboxNumberOfArchers.Text = _mainform._numberOfArcher.ToString();
                    _mainform.tboxHitPowerOfArcher.Text = _mainform._archerHitPower.ToString();
                    #endregion

                    _mainform._currentHealth = _mainform._health;
                    _mainform.lblCurrentHealth.Text = _mainform._currentHealth.ToString();
                    _mainform._unitSize = _mainform._mainPanelSize / 20;
                    _mainform._targetSize = _mainform._mainPanelSize / 28;
                    _mainform._soldierEyeTargetSize = Convert.ToInt32(_mainform._targetSize / (Convert.ToDouble(_mainform._mainPanelSize) / Convert.ToDouble(_mainform._soldiersEyePanelSize)));
                    _mainform._soldierEyeUnitSize = Convert.ToInt32(_mainform._unitSize / (Convert.ToDouble(_mainform._mainPanelSize) / Convert.ToDouble(_mainform._soldiersEyePanelSize)));


                    #region Target located
                    var targetRandomY = _mainform._randomTargetY.Next(_mainform._targetSize, _mainform._mainPanelSize - _mainform._targetSize);
                    _mainform.panelTarget.Location = new Point(_mainform._mainPanelSize - _mainform._targetSize, targetRandomY); //target location set 
                    _mainform.panelTargetSoldiersEye.Location = new Point(_mainform._soldiersEyePanelSize - _mainform._soldierEyeTargetSize, Convert.ToInt32((Convert.ToDouble(targetRandomY) / (Convert.ToDouble(_mainform._mainPanelSize) / Convert.ToDouble(_mainform._soldiersEyePanelSize)))));
                    _mainform.panelTargetSoldiersEye.Tag = "panelTargetSoldiersEye";

                    _mainform.panelTarget.Height = _mainform._targetSize;
                    _mainform.panelTarget.Width = _mainform._targetSize;

                    _mainform.panelTargetSoldiersEye.Height = _mainform._soldierEyeTargetSize;
                    _mainform.panelTargetSoldiersEye.Width = _mainform._soldierEyeTargetSize;
                    _mainform.soldierEyePictureBox.Height = _mainform._soldierEyeUnitSize;
                    _mainform.soldierEyePictureBox.Width = _mainform._soldierEyeUnitSize;
                    int targetX = _mainform.panelTarget.Location.X;
                    int targetY = _mainform.panelTarget.Location.Y;
                    _mainform._targetX = targetX;
                    _mainform._targetY = targetY;
                    _mainform._scenarioTargetLocation = new Point(_mainform._targetX, _mainform._targetY);

                    #endregion


                    #region towers created and locations set
                    PictureBox[] tower = new PictureBox[_mainform._numberOfTowers];
                    Rectangle[] towerSight = new Rectangle[_mainform._numberOfTowers];
                    Rectangle[] listOfKnownTowers = new Rectangle[_mainform._numberOfTowers];
                    Point[] scenarioTowers = new Point[_mainform._numberOfTowers];


                    for (int i = 0; i < _mainform._numberOfTowers; i++)
                    {
                        int randomTowerY = _mainform._randomTowerY.Next(_mainform._mainPanelSize / 10, _mainform._mainPanelSize - (_mainform._unitSize * 2));
                        int randomTowerX = randomTowerY > _mainform._targetY + _mainform._minDistanceFromTower || randomTowerY < Math.Abs(_mainform._targetY - _mainform._minDistanceFromTower) ? _mainform._randomTowerX.Next(_mainform._mainPanelSize / 10, (_mainform._mainPanelSize - (_mainform._unitSize * 2))) : _mainform._randomTowerX.Next(_mainform._mainPanelSize / 10, (_mainform._mainPanelSize - (_mainform._unitSize * 2)) - _mainform._minDistanceFromTower);

                        tower[i] = new PictureBox();
                        tower[i].Location = new Point(randomTowerX, randomTowerY);
                        tower[i].Size = new Size(_mainform._unitSize, _mainform._unitSize);
                        tower[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        tower[i].Visible = true;
                        tower[i].Image = PathPlanning.Properties.Resources.tower;
                        _mainform.panelGodView.Controls.Add(tower[i]);
                        scenarioTowers[i] = tower[i].Location;

                        int towerSightX = tower[i].Location.X - ((_mainform._towerSight * 2) / (700 / _mainform._mainPanelSize) - _mainform._unitSize) / 2;
                        int towerSightY = tower[i].Location.Y - ((_mainform._towerSight * 2) / (700 / _mainform._mainPanelSize) - _mainform._unitSize) / 2;
                        towerSight[i] = new Rectangle(new Point(towerSightX, towerSightY), new Size((_mainform._towerSight * 2) / (700 / _mainform._mainPanelSize), (_mainform._towerSight * 2) / (700 / _mainform._mainPanelSize)));

                    }

                    _mainform._listOfKnownTowers = listOfKnownTowers;
                    _mainform._allTowers = tower;
                    _mainform._allTowerSight = towerSight;
                    _mainform._scenarioTowers = scenarioTowers;
                    #endregion



                    #region archers created and first locations set
                    PictureBox[] archer = new PictureBox[_mainform._numberOfArcher];
                    Rectangle[] archerSightRange = new Rectangle[_mainform._numberOfArcher];
                    Rectangle[] archerHitRange = new Rectangle[_mainform._numberOfArcher];
                    //Rectangle[] listOfKnownArchers = new Rectangle[_numberOfArcher];
                    PictureBox[] listOfKnownArchers = new PictureBox[_mainform._numberOfArcher];
                    Point[] scenarioArcher = new Point[_mainform._numberOfArcher];
                    int[] archerDirection = new int[_mainform._numberOfArcher];
                    int[] archerInitialDirection = new int[_mainform._numberOfArcher];

                    for (int i = 0; i < _mainform._numberOfArcher; i++)
                    {
                        int randomArcherY = _mainform._randomArcherY.Next(_mainform._mainPanelSize / 10, _mainform._mainPanelSize - (_mainform._unitSize * 2));
                        int randomArcherX = randomArcherY > _mainform._targetY + _mainform._minDistanceFromTower || randomArcherY < Math.Abs(_mainform._targetY - _mainform._minDistanceFromTower) ? _mainform._randomArcherX.Next(_mainform._mainPanelSize / 10, (_mainform._mainPanelSize - (_mainform._unitSize * 2))) : _mainform._randomArcherX.Next(_mainform._mainPanelSize / 10, (_mainform._mainPanelSize - (_mainform._unitSize * 2)) - _mainform._minDistanceFromTower);

                        archer[i] = new PictureBox();
                        archer[i].Location = new Point(randomArcherX, randomArcherY);
                        archer[i].Size = new Size(_mainform._unitSize, _mainform._unitSize);
                        archer[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        archer[i].Visible = true;
                        archer[i].Image = PathPlanning.Properties.Resources.archer;
                        _mainform.panelGodView.Controls.Add(archer[i]);
                        scenarioArcher[i] = archer[i].Location;

                        archerSightRange[i] = new Rectangle();
                        int archerSightX = archer[i].Location.X - ((_mainform._archerSightRange * 2) / (700 / _mainform._mainPanelSize) - _mainform._unitSize) / 2;
                        int archerSightY = archer[i].Location.Y - ((_mainform._archerSightRange * 2) / (700 / _mainform._mainPanelSize) - _mainform._unitSize) / 2;
                        archerSightRange[i].Location = new Point(archerSightX, archerSightY);
                        archerSightRange[i].Size = new Size((_mainform._archerSightRange * 2) / (700 / _mainform._mainPanelSize), (_mainform._archerSightRange * 2) / (700 / _mainform._mainPanelSize));

                        archerHitRange[i] = new Rectangle();
                        int archerHitX = archer[i].Location.X - ((_mainform._archerHitRange * 2) / (700 / _mainform._mainPanelSize) - _mainform._unitSize) / 2;
                        int archerHitY = archer[i].Location.Y - ((_mainform._archerHitRange * 2) / (700 / _mainform._mainPanelSize) - _mainform._unitSize) / 2;
                        archerHitRange[i].Location = new Point(archerHitX, archerHitY);
                        archerHitRange[i].Size = new Size((_mainform._archerHitRange * 2) / (700 / _mainform._mainPanelSize), (_mainform._archerHitRange * 2) / (700 / _mainform._mainPanelSize));

                        int x = _mainform._archerRandomMoveDirection.Next(0, 10);
                        archerDirection[i] = x < 5 ? archerDirection[i] = 1 : archerDirection[i] = 0;
                        archerInitialDirection[i] = archerDirection[i];
                    }

                    _mainform._allArchers = archer;
                    _mainform._allArcherSightRange = archerSightRange;
                    _mainform._allArcherHitRange = archerHitRange;

                    _mainform._listOfKnownArchers = listOfKnownArchers;
                    _mainform._archerForSoldierEye = listOfKnownArchers;

                    _mainform._scenarioArchers = scenarioArcher;
                    _mainform._archerDirection = archerDirection;

                    _mainform._archerInitialDirection = archerInitialDirection;
                    _mainform._scenarioArcherFirstDirection = _mainform._archerInitialDirection;

                    #endregion



                    #region soldiers created and locations set
                    PictureBox[] soldier = new PictureBox[_mainform._numberOfSoldiers];
                    Point[] scneraioSoldier = new Point[_mainform._numberOfSoldiers];
                    Rectangle[] soldierSight = new Rectangle[_mainform._numberOfSoldiers];

                    for (int i = 0; i < _mainform._numberOfSoldiers; i++)
                    {
                        var randomSoldierY = _mainform._randomSoldierY.Next(0, _mainform._mainPanelSize - _mainform._unitSize);
                        soldier[i] = new PictureBox();
                        soldier[i].Size = new Size(_mainform._unitSize, _mainform._unitSize);
                        soldier[i].Location = new Point(0, randomSoldierY);
                        soldier[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        soldier[i].Image = PathPlanning.Properties.Resources.soldier;
                        scneraioSoldier[i] = soldier[i].Location;
                        _mainform.panelGodView.Controls.Add(soldier[i]);

                        soldierSight[i] = new Rectangle();
                        int soldierSightX = soldier[i].Location.X - (((_mainform._soldierSight * 2) / (700 / _mainform._mainPanelSize)) - _mainform._unitSize) / 2;
                        int soldierSightY = soldier[i].Location.Y - (((_mainform._soldierSight * 2) / (700 / _mainform._mainPanelSize)) - _mainform._unitSize) / 2;
                        soldierSight[i].Location = new Point(soldierSightX, soldierSightY);
                        soldierSight[i].Size = new Size((_mainform._soldierSight * 2) / (700 / _mainform._mainPanelSize), (_mainform._soldierSight * 2) / (700 / _mainform._mainPanelSize));
                    }

                    //_currentSoldierSight = soldierSight[0];
                    _mainform._allSoldierSight = soldierSight;

                    _mainform._allSoldiers = soldier;
                    _mainform._currentSoldier = soldier[0];
                    _mainform._scenarioSoldiers = scneraioSoldier;
                    _mainform.soldierEyePictureBox.Location = new Point(soldier[0].Location.X, Convert.ToInt32((Convert.ToDouble(soldier[0].Location.Y) / (Convert.ToDouble(_mainform._mainPanelSize) / Convert.ToDouble(_mainform._soldiersEyePanelSize)))));
                    _mainform.soldierEyePictureBox.Tag = "soldierEyePictureBox";
                    #endregion




                    Rectangle[] listOfAllObtacles = new Rectangle[_mainform._numberOfArcher + _mainform._numberOfTowers];
                    _mainform._listOfAllObstacles = listOfAllObtacles;
                    _mainform._allObstacles = new Rectangle[_mainform._numberOfTowers + _mainform._numberOfArcher];
                    _mainform._allArcherSightRange.CopyTo(_mainform._allObstacles, 0);
                    _mainform._allTowerSight.CopyTo(_mainform._allObstacles, _mainform._numberOfArcher);

                    _mainform.Invalidate();
                }
                else
                {
                    MessageBox.Show("Wrong Input!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _mainform._archerHitRange = 35;
                    _mainform._archerSightRange = 70;
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
