using PathFinding;
using System.Text;

namespace PathPlanning.Utilities
{
    public class Scenario
    {
        MainForm _mainform;

        public Scenario(MainForm mainform)
        {
            _mainform = mainform;
        }

        public Scenario()
        {

        }

        public void SaveScenario(Point scenarioTargetLocation, Point[] scenarioSoldiers, Point[] scenarioArchers, Point[] scenarioTowers, int[] scenarioUserVariables, int[] scenarioArcherFirstDirection)
        {
            StringBuilder scenarioUserVariablesBuilder = new StringBuilder();
            StringBuilder scenarioSoldiersBuilder = new StringBuilder();
            StringBuilder scenarioArchersBuilder = new StringBuilder();
            StringBuilder scenarioTowersBuilder = new StringBuilder();
            StringBuilder scenarioArcherFirstDirectionBulder = new StringBuilder();

            if (MessageBox.Show("Do you want to save the scenario?", "Save into a new file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = @"C:\txt";
                saveFileDialog.Filter = "Text Files Only (*.txt) | *.txt";
                saveFileDialog.DefaultExt = "txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Stream stream = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                    using (StreamWriter streamWriter = new StreamWriter(stream))
                    {
                        foreach (var item in scenarioUserVariables)
                        {
                            scenarioUserVariablesBuilder.Append(Convert.ToString(item.ToString()) + " ");
                        }

                        foreach (var item in scenarioSoldiers)
                        {
                            scenarioSoldiersBuilder.Append(item.X.ToString() + "," + item.Y.ToString() + ";");
                        }
                        foreach (var item in scenarioArchers)
                        {
                            scenarioArchersBuilder.Append(item.X.ToString() + "," + item.Y.ToString() + ";");
                        }
                        foreach (var item in scenarioTowers)
                        {
                            scenarioTowersBuilder.Append(item.X.ToString() + "," + item.Y.ToString() + ";");
                        }
                        foreach (var item in scenarioArcherFirstDirection)
                        {
                            scenarioArcherFirstDirectionBulder.Append(item.ToString() + " ");

                        }
                        streamWriter.WriteLine(scenarioUserVariablesBuilder.ToString());
                        streamWriter.WriteLine(scenarioTargetLocation.X.ToString() + "," + scenarioTargetLocation.Y.ToString() + ";");
                        streamWriter.WriteLine(scenarioSoldiersBuilder.ToString());
                        streamWriter.WriteLine(scenarioArchersBuilder.ToString());
                        streamWriter.WriteLine(scenarioTowersBuilder.ToString());
                        streamWriter.WriteLine(scenarioArcherFirstDirectionBulder.ToString());


                        MessageBox.Show("Succesfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void OpenScenario()
        {


            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"C:\txt";
            openFile.Filter = "Text Files Only (*.txt) | *.txt";
            openFile.DefaultExt = "txt";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                StringBuilder stringBuilder = new StringBuilder();
                _mainform._scenarioUserVariables = new int[14];


                using (StreamReader reader = new StreamReader(openFile.OpenFile()))
                {
                    string userVariables = reader.ReadLine();
                    string[] userVariablesSplitted = userVariables.Split(' ').ToArray();
                    for (int i = 0; i < 14; i++)
                    {
                        _mainform._scenarioUserVariables[i] = Convert.ToInt32(userVariablesSplitted[i]);
                    }

                    string targetLoc = reader.ReadLine();
                    if (targetLoc.Length > 0)
                    {
                        var targetSplitted = targetLoc.Substring(0, targetLoc.Length - 1).Split(';').Select(x => x.Split(',')).Select(y => new Point(Convert.ToInt32(y[0]), Convert.ToInt32(y[1]))).ToArray();
                        _mainform._scenarioTargetLocation = targetSplitted[0];
                    }

                    string soldierLocations = reader.ReadLine();
                    if (soldierLocations.Length > 0)
                    {
                        var soldierLocationsSplitted = soldierLocations.Substring(0, soldierLocations.Length - 1).Split(';').Select(y => y.Split(',')).Select(y => new Point(int.Parse(y[0]), Convert.ToInt32(y[1]))).ToArray();
                        _mainform._scenarioSoldiers = soldierLocationsSplitted;
                        for (int i = 0; i < soldierLocationsSplitted.Length; i++)
                        {
                            _mainform._scenarioSoldiers[i] = soldierLocationsSplitted[i];
                        }
                    }


                    string archerLocations = reader.ReadLine();
                    if (archerLocations.Length > 0)
                    {
                        var archerLocationsSplitted = archerLocations.Substring(0, archerLocations.Length - 1).Split(';').Select(y => y.Split(',')).Select(y => new Point(Convert.ToInt32(y[0]), Convert.ToInt32(y[1]))).ToArray();
                        _mainform._scenarioArchers = archerLocationsSplitted;
                        for (int i = 0; i < archerLocationsSplitted.Length; i++)
                        {
                            _mainform._scenarioArchers[i] = archerLocationsSplitted[i];
                        }
                    }

                    string towerLocations = reader.ReadLine();
                    if (towerLocations.Length > 0)
                    {
                        var towerLocationsSplitted = towerLocations.Substring(0, towerLocations.Length - 1).Split(';').Select(y => y.Split(',')).Select(y => new Point(Convert.ToInt32(y[0]), Convert.ToInt32(y[1]))).ToArray();
                        _mainform._scenarioTowers = towerLocationsSplitted;
                        for (int i = 0; i < towerLocationsSplitted.Length; i++)
                        {
                            _mainform._scenarioTowers[i] = towerLocationsSplitted[i];
                        }
                    }


                    string archerDirection = reader.ReadLine();
                    if (archerDirection.Length > 0)
                    {
                        string[] archerDirectionSplitted = archerDirection.Substring(0, archerDirection.Length - 1).Split(' ').ToArray();
                        _mainform._scenarioArcherFirstDirection = new int[archerDirectionSplitted.Length];
                        for (int i = 0; i < archerDirectionSplitted.Length; i++)
                        {
                            _mainform._scenarioArcherFirstDirection[i] = Convert.ToInt32(archerDirectionSplitted[i]);
                        }
                    }

                }


                _mainform._mainPanelSize = _mainform._scenarioUserVariables[0];
                _mainform.tboxPanelSize.Text = _mainform._mainPanelSize.ToString();
                _mainform._archerSightRange = _mainform._scenarioUserVariables[1];
                _mainform.tboxArcherSight.Text = _mainform._archerSightRange.ToString();
                _mainform._archerHitRange = _mainform._scenarioUserVariables[2];
                _mainform.tboxHitRangeOfArcher.Text = _mainform._archerHitRange.ToString();
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

                    #region UserVariables
                    _mainform._soldierMaxMovement = _mainform._scenarioUserVariables[3];
                    _mainform.tboxSoldierMaxMovement.Text = _mainform._soldierMaxMovement.ToString();
                    _mainform._archerMaxMovement = _mainform._scenarioUserVariables[4];
                    _mainform.tboxMaxMovementOfArcher.Text = _mainform._archerMaxMovement.ToString();
                    _mainform._numberOfSoldiers = _mainform._scenarioUserVariables[5];
                    _mainform.tboxNumberOfSoldiers.Text = _mainform._numberOfSoldiers.ToString();
                    _mainform._numberOfTowers = _mainform._scenarioUserVariables[6];
                    _mainform.tboxNumberOfTowers.Text = _mainform._numberOfTowers.ToString();
                    _mainform._towerSight = _mainform._scenarioUserVariables[7];
                    _mainform.tboxSightRangeOfTowers.Text = _mainform._towerSight.ToString();
                    _mainform._soldierSight = _mainform._scenarioUserVariables[8];
                    _mainform.tboxSightOfSoldier.Text = _mainform._soldierSight.ToString();
                    _mainform._health = _mainform._scenarioUserVariables[9];
                    _mainform.tboxHealthOfSoldier.Text = _mainform._health.ToString();
                    _mainform._towerHitPower = _mainform._scenarioUserVariables[10];
                    _mainform.tboxHitPowerOfTower.Text = _mainform._towerHitPower.ToString();
                    _mainform._minDistanceFromTower = _mainform._scenarioUserVariables[11];
                    _mainform.tboxMinDistanceFromTarget.Text = _mainform._minDistanceFromTower.ToString();
                    _mainform._numberOfArcher = _mainform._scenarioUserVariables[12];
                    _mainform.tboxNumberOfArchers.Text = _mainform._numberOfArcher.ToString();
                    _mainform._archerHitPower = _mainform._scenarioUserVariables[13];
                    _mainform.tboxHitPowerOfArcher.Text = _mainform._archerHitPower.ToString();
                    #endregion


                    _mainform._currentHealth = _mainform._health;
                    _mainform.lblCurrentHealth.Text = _mainform._currentHealth.ToString();
                    _mainform._unitSize = _mainform._mainPanelSize / 20;
                    _mainform._targetSize = _mainform._mainPanelSize / 28;
                    _mainform._soldierEyeTargetSize = Convert.ToInt32(_mainform._targetSize / (Convert.ToDouble(_mainform._mainPanelSize) / Convert.ToDouble(_mainform._soldiersEyePanelSize)));
                    _mainform._soldierEyeUnitSize = Convert.ToInt32(_mainform._unitSize / (Convert.ToDouble(_mainform._mainPanelSize) / Convert.ToDouble(_mainform._soldiersEyePanelSize)));


                    #region Target located
                    _mainform.panelTarget.Location = _mainform._scenarioTargetLocation;

                    _mainform.panelTargetSoldiersEye.Location = new Point(_mainform._soldiersEyePanelSize - _mainform._soldierEyeTargetSize, Convert.ToInt32((Convert.ToDouble(_mainform._scenarioTargetLocation.Y) / (Convert.ToDouble(_mainform._mainPanelSize) / Convert.ToDouble(_mainform._soldiersEyePanelSize)))));
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

                    #endregion


                    #region towers created and locations set
                    PictureBox[] tower = new PictureBox[_mainform._numberOfTowers];
                    Rectangle[] towerSight = new Rectangle[_mainform._numberOfTowers];
                    Rectangle[] listOfKnownTowers = new Rectangle[_mainform._numberOfTowers];


                    for (int i = 0; i < _mainform._numberOfTowers; i++)
                    {
                        int randomTowerY = _mainform._randomTowerY.Next(_mainform._mainPanelSize / 10, _mainform._mainPanelSize - (_mainform._unitSize * 2));
                        int randomTowerX = randomTowerY > _mainform._targetY + _mainform._minDistanceFromTower || randomTowerY < Math.Abs(_mainform._targetY - _mainform._minDistanceFromTower) ? _mainform._randomTowerX.Next(_mainform._mainPanelSize / 10, (_mainform._mainPanelSize - (_mainform._unitSize * 2))) : _mainform._randomTowerX.Next(_mainform._mainPanelSize / 10, (_mainform._mainPanelSize - (_mainform._unitSize * 2)) - _mainform._minDistanceFromTower);

                        tower[i] = new PictureBox();
                        tower[i].Location = _mainform._scenarioTowers[i];//new Point(randomTowerX, randomTowerY);
                        tower[i].Size = new Size(_mainform._unitSize, _mainform._unitSize);
                        tower[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        tower[i].Visible = true;
                        tower[i].Image = PathPlanning.Properties.Resources.tower;
                        _mainform.panelGodView.Controls.Add(tower[i]);


                        int towerSightX = tower[i].Location.X - ((_mainform._towerSight * 2) / (700 / _mainform._mainPanelSize) - _mainform._unitSize) / 2;
                        int towerSightY = tower[i].Location.Y - ((_mainform._towerSight * 2) / (700 / _mainform._mainPanelSize) - _mainform._unitSize) / 2;
                        towerSight[i] = new Rectangle(new Point(towerSightX, towerSightY), new Size((_mainform._towerSight * 2) / (700 / _mainform._mainPanelSize), (_mainform._towerSight * 2) / (700 / _mainform._mainPanelSize)));

                    }

                    _mainform._listOfKnownTowers = listOfKnownTowers;
                    _mainform._allTowers = tower;
                    _mainform._allTowerSight = towerSight;

                    #endregion



                    #region archers created and first locations set
                    PictureBox[] archer = new PictureBox[_mainform._numberOfArcher];
                    Rectangle[] archerSightRange = new Rectangle[_mainform._numberOfArcher];
                    Rectangle[] archerHitRange = new Rectangle[_mainform._numberOfArcher];
                    PictureBox[] archerForSoldierEye = new PictureBox[_mainform._numberOfArcher];

                    //Rectangle[] listOfKnownArchers = new Rectangle[_numberOfArcher];
                    PictureBox[] listOfKnownArchers = new PictureBox[_mainform._numberOfArcher];
                    int[] archerFirstDirection = new int[_mainform._numberOfArcher];
                    for (int i = 0; i < _mainform._numberOfArcher; i++)
                    {
                        int randomArcherY = _mainform._randomArcherY.Next(_mainform._mainPanelSize / 10, _mainform._mainPanelSize - (_mainform._unitSize * 2));
                        int randomArcherX = randomArcherY > _mainform._targetY + _mainform._minDistanceFromTower || randomArcherY < Math.Abs(_mainform._targetY - _mainform._minDistanceFromTower) ? _mainform._randomArcherX.Next(_mainform._mainPanelSize / 10, (_mainform._mainPanelSize - (_mainform._unitSize * 2))) : _mainform._randomArcherX.Next(_mainform._mainPanelSize / 10, (_mainform._mainPanelSize - (_mainform._unitSize * 2)) - _mainform._minDistanceFromTower);

                        archer[i] = new PictureBox();
                        archer[i].Location = _mainform._scenarioArchers[i];//new Point(randomArcherX, randomArcherY);
                        archer[i].Size = new Size(_mainform._unitSize, _mainform._unitSize);
                        archer[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        archer[i].Visible = true;
                        archer[i].Image = PathPlanning.Properties.Resources.archer;
                        _mainform.panelGodView.Controls.Add(archer[i]);

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
                        archerFirstDirection[i] = _mainform._scenarioArcherFirstDirection[i];

                    }

                    _mainform._allArchers = archer;
                    _mainform._allArcherSightRange = archerSightRange;
                    _mainform._allArcherHitRange = archerHitRange;
                    _mainform._listOfKnownArchers = listOfKnownArchers;
                    _mainform._archerForSoldierEye = archerForSoldierEye;
                    _mainform._archerDirection = archerFirstDirection;

                    #endregion



                    #region soldiers created and locations set
                    PictureBox[] soldier = new PictureBox[_mainform._numberOfSoldiers];
                    Rectangle[] soldierSight = new Rectangle[_mainform._numberOfSoldiers];

                    for (int i = 0; i < _mainform._numberOfSoldiers; i++)
                    {
                        var randomSoldierY = _mainform._randomSoldierY.Next(0, _mainform._mainPanelSize - _mainform._unitSize);
                        soldier[i] = new PictureBox();
                        soldier[i].Size = new Size(_mainform._unitSize, _mainform._unitSize);
                        soldier[i].Location = _mainform._scenarioSoldiers[i];//new Point(0, randomSoldierY);
                        soldier[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        soldier[i].Image = PathPlanning.Properties.Resources.soldier;
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
                    _mainform.soldierEyePictureBox.Location = new Point(soldier[0].Location.X, Convert.ToInt32((Convert.ToDouble(soldier[0].Location.Y) / (Convert.ToDouble(_mainform._mainPanelSize) / Convert.ToDouble(_mainform._soldiersEyePanelSize)))));
                    _mainform.soldierEyePictureBox.Tag = "soldierEyePictureBox";
                    #endregion


                    Rectangle[] listOfAllObtacles = new Rectangle[_mainform._numberOfArcher + _mainform._numberOfTowers];
                    _mainform._listOfAllObstacles = listOfAllObtacles;
                    _mainform._allObstacles = new Rectangle[_mainform._numberOfTowers + _mainform._numberOfArcher];
                    //_numberOfObstacles = _numberOfArcher + _numberOfTowers;
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

        }


    }
}
