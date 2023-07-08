using System.Text;

namespace PathFinding
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        #region Variables
        private int _moveSoldierY;
        private int _moveSoldierX;

        private int _currentSoldierNumber;
        private int _currentSoldierX;
        private int _currentSoldierY;
        private int _currentHealth;

        Rectangle[] _listOfKnownTowers;
        PictureBox[] _listOfKnownArchers;
        Rectangle[] _listOfAllObstacles;

        private int _knownTowerNumber = 0;
        private int _knownArcherNumber = 0;
        private int _knownObstacleNumber = 0;
        private bool _isSoldierAlreadyGoneToX = false;
        private bool _isSoldierStuck;
        private int _soldierStuckNumber = 0;
        #endregion
        private void AddArcherToList(int archerNumber)
        {
            _listOfKnownArchers[archerNumber] = new PictureBox();
            _listOfKnownArchers[archerNumber] = _allArchers[archerNumber];
            _listOfAllObstacles[_knownObstacleNumber] = new Rectangle();
            _listOfAllObstacles[_knownObstacleNumber] = _allObstacles[archerNumber];

            PictureBox archerForSoldierEye = new PictureBox
            {
                Image = PathPlanning.Properties.Resources.smallArcher,
                Visible = true,
                Name = "obstacle",
                Size = new Size(_soldierEyeUnitSize, _soldierEyeUnitSize),
                Location = new Point(Convert.ToInt32((Convert.ToDouble(_allArchers[archerNumber].Location.X) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))), Convert.ToInt32((Convert.ToDouble(_allArchers[archerNumber].Location.Y) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize))))),
            };
            panelSoldiersEye.Controls.Add(archerForSoldierEye);
            _archerForSoldierEye[archerNumber] = archerForSoldierEye;

            _knownArcherNumber++;
            _knownObstacleNumber++;
        }

        private void AddTowerToList(int towerSightNumber)
        {
            _listOfKnownTowers[_knownTowerNumber] = new Rectangle();
            _listOfKnownTowers[_knownTowerNumber] = _allTowerSight[towerSightNumber];
            _listOfAllObstacles[_knownObstacleNumber] = new Rectangle();
            _listOfAllObstacles[_knownObstacleNumber] = _allObstacles[towerSightNumber];
            PictureBox towerForSoldierEye = new PictureBox();
            towerForSoldierEye.Image = PathPlanning.Properties.Resources.smallTower;
            towerForSoldierEye.Visible = true;
            towerForSoldierEye.Name = "obstacle";
            towerForSoldierEye.Size = new Size(_soldierEyeUnitSize, _soldierEyeUnitSize);
            towerForSoldierEye.Location = new Point(Convert.ToInt32((Convert.ToDouble(_allTowers[towerSightNumber].Location.X) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))), Convert.ToInt32((Convert.ToDouble(_allTowers[towerSightNumber].Location.Y) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))));
            panelSoldiersEye.Controls.Add(towerForSoldierEye);
            _knownTowerNumber++;
            _knownObstacleNumber++;
        }

        private void mainTimer_Tick_1(object sender, EventArgs e)
        {
            Graphics g = this.panelGodView.CreateGraphics();
            g.Clear(Color.White); //makes the soldierSight clean (because soldier is moving)
            this.Invalidate();   //prevents circles to be erased

            _currentSoldierX = _currentSoldier.Location.X;
            _currentSoldierY = _currentSoldier.Location.Y;
            _moveSoldierX = _currentSoldierX;
            _moveSoldierY = _currentSoldierY;
            Rectangle soldierSight = _allSoldierSight[_currentSoldierNumber];
            Rectangle soldier = new Rectangle(_currentSoldierX, _currentSoldierY, _unitSize, _unitSize);
            Rectangle soldierOneStepBefore = new Rectangle(_currentSoldierX - _soldierMaxMovement, _currentSoldierY, _unitSize, _unitSize);
            Rectangle soldierOneStepForward = new Rectangle(_currentSoldierX + _defaultStep, _currentSoldierY, _unitSize, _unitSize);


            for (int i = 0; i < _numberOfArcher; i++)
            {
                if (_listOfKnownArchers[i] != null)//soldiers eye map archer lcoation
                {
                    _archerForSoldierEye[i].Location = new Point(Convert.ToInt32((Convert.ToDouble(_allArchers[i].Location.X) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))), Convert.ToInt32((Convert.ToDouble(_allArchers[i].Location.Y) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))));
                }
                if (_allArcherHitRange[i].IntersectsWith(soldier) == false)
                {
                    if (_archerDirection[i] == 1)
                    {
                        if (_allArchers[i].Location.Y <= 0)
                        {
                            ArcherGoDownDefaultStep(_allArchers[i]);
                            _archerDirection[i] = 0;
                        }
                        else
                        {
                            ArcherGoUpDefaultStep(_allArchers[i]);
                        }

                    }
                    else if (_archerDirection[i] == 0)
                    {
                        if (_allArchers[i].Location.Y + _unitSize >= _mainPanelSize)
                        {
                            ArcherGoUpDefaultStep(_allArchers[i]);
                            _archerDirection[i] = 1;
                        }
                        else
                        {
                            ArcherGoDownDefaultStep(_allArchers[i]);
                        }
                    }
                }

            }//archer patrolling


            for (int i = 0; i < _numberOfTowers; i++)
            {
                int towerX = _allTowers[i].Location.X;
                int towerY = _allTowers[i].Location.Y;
                Rectangle tower = new Rectangle(towerX, towerY, _unitSize, _unitSize);

                if (_allSoldierSight[_currentSoldierNumber].IntersectsWith(tower) == false)
                {
                    if (_allTowerSight[i].IntersectsWith(soldierOneStepForward) == true)
                    {
                        if (_listOfKnownTowers.Contains(_allTowerSight[i]) == false)
                        {
                            //TowerHitsToSoldier();
                            if (_currentHealth - _towerHitPower > 0)
                            {
                                _currentHealth -= _towerHitPower;
                                lblCurrentHealth.Text = _currentHealth.ToString();
                            }
                            else
                            {
                                mainTimer.Stop();
                                _allSoldiers[_currentSoldierNumber].Visible = false; // ölen asker gözükmeyecek
                                _currentSoldierNumber++;
                                _currentHealth = _health;

                                if (_currentSoldierNumber < _numberOfSoldiers)
                                {
                                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                                    _moveSoldierX = 0;
                                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                                    _currentSoldierX = _moveSoldierX;
                                    _currentSoldierY = _moveSoldierY;

                                    lblCurrentHealth.Text = _health.ToString();

                                    mainTimer.Start();
                                }
                                else
                                {
                                    mainTimer.Stop();
                                    _currentSoldierNumber--;
                                    lblCurrentHealth.Text = "0";
                                    MessageBox.Show(@"No soldier left!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                return;
                            }
                        }
                    }
                    if (_listOfKnownTowers.Contains(_allTowerSight[i]) == true)
                    {
                        if (_allTowerSight[i].IntersectsWith(soldierOneStepBefore) == true)
                        {
                            //TowerHitsToSoldier();
                            if (_currentHealth - _towerHitPower > 0)
                            {
                                _currentHealth -= _towerHitPower;
                                lblCurrentHealth.Text = _currentHealth.ToString();
                            }
                            else
                            {
                                mainTimer.Stop();
                                _allSoldiers[_currentSoldierNumber].Visible = false; // ölen asker gözükmeyecek
                                _currentSoldierNumber++;
                                _currentHealth = _health;

                                if (_currentSoldierNumber < _numberOfSoldiers)
                                {
                                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                                    _moveSoldierX = 0;
                                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                                    _currentSoldierX = _moveSoldierX;
                                    _currentSoldierY = _moveSoldierY;

                                    lblCurrentHealth.Text = _health.ToString();

                                    mainTimer.Start();
                                }
                                else
                                {
                                    mainTimer.Stop();
                                    _currentSoldierNumber--;
                                    lblCurrentHealth.Text = "0";
                                    MessageBox.Show(@"No soldier left!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                return;
                            }
                        }
                    }
                }
                else if (_allSoldierSight[_currentSoldierNumber].IntersectsWith(tower) == true) //2.20 07/06/23
                {
                    if (_listOfKnownTowers.Contains(_allTowerSight[i]) == true)
                    {
                        if (_allTowerSight[i].IntersectsWith(soldierOneStepBefore) == true)
                        {
                            //TowerHitsToSoldier();
                            if (_currentHealth - _towerHitPower > 0)
                            {
                                _currentHealth -= _towerHitPower;
                                lblCurrentHealth.Text = _currentHealth.ToString();
                            }
                            else
                            {
                                mainTimer.Stop();
                                _allSoldiers[_currentSoldierNumber].Visible = false; // ölen asker gözükmeyecek
                                _currentSoldierNumber++;
                                _currentHealth = _health;

                                if (_currentSoldierNumber < _numberOfSoldiers)
                                {
                                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                                    _moveSoldierX = 0;
                                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                                    _currentSoldierX = _moveSoldierX;
                                    _currentSoldierY = _moveSoldierY;

                                    lblCurrentHealth.Text = _health.ToString();

                                    mainTimer.Start();
                                }
                                else
                                {
                                    mainTimer.Stop();
                                    _currentSoldierNumber--;
                                    lblCurrentHealth.Text = "0";
                                    MessageBox.Show(@"No soldier left!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                return;
                            }
                        }
                    }
                }
            }//only hit


            for (int i = 0; i < _numberOfArcher; i++)
            {
                Rectangle archer = new Rectangle(_allArchers[i].Location.X, _allArchers[i].Location.Y, _unitSize, _unitSize);
                if (_listOfKnownArchers[i] == null)
                {
                    if (_allSoldierSight[_currentSoldierNumber].IntersectsWith(archer) == false)
                    {
                        if (_allArcherHitRange[i].IntersectsWith(soldier) == true)
                        {
                            // ArcherHitsToSoldier();
                            if (_currentHealth - _archerHitPower > 0)
                            {
                                _currentHealth -= _archerHitPower;
                                lblCurrentHealth.Text = _currentHealth.ToString();
                            }
                            else
                            {
                                mainTimer.Stop();
                                _allSoldiers[_currentSoldierNumber].Visible = false;
                                _currentHealth = _health;
                                _currentSoldierNumber++;

                                if (_currentSoldierNumber < _numberOfSoldiers)
                                {
                                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                                    _moveSoldierX = 0;
                                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                                    _currentSoldierX = _moveSoldierX;
                                    _currentSoldierY = _moveSoldierY;

                                    lblCurrentHealth.Text = _health.ToString();

                                    mainTimer.Start();
                                }
                                else
                                {
                                    mainTimer.Stop();
                                    _currentSoldierNumber--;
                                    lblCurrentHealth.Text = "0";
                                    MessageBox.Show(@"No soldier left!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                return;
                            }
                        }
                    }
                }
                if (_listOfKnownArchers[i] != null)
                {
                    if (_allArcherHitRange[i].IntersectsWith(soldier) == true)
                    {
                        //ArcherHitsToSoldier();
                        if (_currentHealth - _archerHitPower > 0)
                        {
                            _currentHealth -= _archerHitPower;
                            lblCurrentHealth.Text = _currentHealth.ToString();
                        }
                        else
                        {
                            mainTimer.Stop();
                            _allSoldiers[_currentSoldierNumber].Visible = false;
                            _currentHealth = _health;
                            _currentSoldierNumber++;

                            if (_currentSoldierNumber < _numberOfSoldiers)
                            {
                                _currentSoldier = _allSoldiers[_currentSoldierNumber];
                                _moveSoldierX = 0;
                                _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                                _currentSoldierX = _moveSoldierX;
                                _currentSoldierY = _moveSoldierY;

                                lblCurrentHealth.Text = _health.ToString();

                                mainTimer.Start();
                            }
                            else
                            {
                                mainTimer.Stop();
                                _currentSoldierNumber--;
                                lblCurrentHealth.Text = "0";
                                MessageBox.Show(@"No soldier left!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            return;
                        }
                    }
                }
            }//only hit 


            for (int i = 0; i < _numberOfTowers; i++)
            {
                int towerX = _allTowers[i].Location.X;
                int towerY = _allTowers[i].Location.Y;
                Rectangle tower = new Rectangle(towerX, towerY, _unitSize, _unitSize);

                if (_allSoldierSight[_currentSoldierNumber].IntersectsWith(tower) == true)
                {
                    if (_listOfKnownTowers.Contains(_allTowerSight[i]) == false)
                    {
                        AddTowerToList(i);
                        if (_allTowerSight[i].IntersectsWith(soldier) == true)
                        {
                            AvoidFromKnownTower(_allTowers[i]);
                            TowerHitsToSoldier();
                            return;
                        }
                    }
                }
            }//add to list


            for (int i = 0; i < _numberOfArcher; i++)
            {
                int archerX = _allArchers[i].Location.X;
                int archerY = _allArchers[i].Location.Y;
                Rectangle archer = new Rectangle(archerX, archerY, _unitSize, _unitSize);

                // if (_listOfKnownArchers.Contains(_allArcherSightRange[i]) == false)
                if (_listOfKnownArchers[i] == null)
                {
                    if (_allSoldierSight[_currentSoldierNumber].IntersectsWith(archer) == true)
                    {
                        AddArcherToList(i);
                        AvoidFromArcher(_allArchers[i]);
                        return;
                    }
                    if (_allArcherHitRange[i].IntersectsWith(soldier) == true)
                    {
                        //ArcherHitsToSoldier();
                        if (_currentHealth - _archerHitPower > 0)
                        {
                            _currentHealth -= _archerHitPower;
                            lblCurrentHealth.Text = _currentHealth.ToString();
                        }
                        else
                        {
                            mainTimer.Stop();
                            _allSoldiers[_currentSoldierNumber].Visible = false;
                            _currentHealth = _health;
                            _currentSoldierNumber++;

                            if (_currentSoldierNumber < _numberOfSoldiers)
                            {
                                _currentSoldier = _allSoldiers[_currentSoldierNumber];
                                _moveSoldierX = 0;
                                _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                                _currentSoldierX = _moveSoldierX;
                                _currentSoldierY = _moveSoldierY;

                                lblCurrentHealth.Text = _health.ToString();

                                mainTimer.Start();
                            }
                            else
                            {
                                mainTimer.Stop();
                                _currentSoldierNumber--;
                                lblCurrentHealth.Text = "0";
                                MessageBox.Show(@"No soldier left!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            return;
                        }
                    }
                    else if (_allArcherSightRange[i].IntersectsWith(soldier) == true)
                    {
                        ArcherMoveMaxStep(_allArchers[i], _allSoldiers[_currentSoldierNumber]);
                    }
                }
            }//add to list


            for (int i = 0; i < _numberOfArcher; i++)
            {
                if (_listOfKnownArchers[i] != null) //else
                {
                    if (_allArcherSightRange[i].IntersectsWith(soldier) == true)
                    {
                        if (_allArcherHitRange[i].IntersectsWith(soldier) == true)
                        {
                            AvoidFromArcher(_allArchers[i]);
                            ArcherHitsToSoldier();
                            return;
                        }
                        AvoidFromArcher(_allArchers[i]);
                        return;
                    }
                    if (_allArcherSightRange[i].IntersectsWith(soldierSight) == true)
                    {
                        if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
                        {
                            UpOrDownSoldierAccordingToTarget();
                            return;
                        }
                        if (_allArcherSightRange[i].IntersectsWith(soldierOneStepBefore) == false)
                        {
                            //if (_allArchers[i].Location.Y > _allSoldiers[_currentSoldierNumber].Location.Y)
                            if (_archerDirection[i] == 0)
                            {
                                if (_allArcherSightRange[i].Top > _allSoldiers[_currentSoldierNumber].Location.Y + _unitSize)
                                {
                                    if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
                                    {
                                        UpOrDownSoldierAccordingToTarget();
                                        return;
                                    }
                                    _moveSoldierX += _soldierMaxMovement;//_defaultStep; 
                                    StepSoldier(_moveSoldierX, _allSoldiers[_currentSoldierNumber].Location.Y);
                                    return;
                                }
                                if (_currentSoldier.Location.Y <= 0)//
                                {
                                    _moveSoldierX += _soldierMaxMovement; ;
                                    StepSoldier(_moveSoldierX, _allSoldiers[_currentSoldierNumber].Location.Y);
                                    return;
                                }
                                _moveSoldierY -= _defaultStep;
                                StepSoldier(_allSoldiers[_currentSoldierNumber].Location.X, _moveSoldierY);
                                return;

                            }

                            if (_archerDirection[i] == 1)
                            {
                                if (_allArcherSightRange[i].Bottom < _allSoldiers[_currentSoldierNumber].Location.Y)
                                {
                                    if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
                                    {
                                        UpOrDownSoldierAccordingToTarget();
                                        return;
                                    }
                                    _moveSoldierX += _soldierMaxMovement;//_defaultStep; 
                                    StepSoldier(_moveSoldierX, _allSoldiers[_currentSoldierNumber].Location.Y);
                                    return;

                                }
                                if (_currentSoldier.Location.Y + _unitSize >= _mainPanelSize)
                                {
                                    _moveSoldierX += _soldierMaxMovement; ;
                                    StepSoldier(_moveSoldierX, _allSoldiers[_currentSoldierNumber].Location.Y);
                                    return;
                                }
                                _moveSoldierY += _defaultStep; ;
                                StepSoldier(_allSoldiers[_currentSoldierNumber].Location.X, _moveSoldierY);
                                return;
                            }
                        }
                    }
                }
            }//general archer


            for (int i = 0; i < _numberOfTowers; i++) //tower hits and soldier does not see
            {
                if (_listOfKnownTowers.Contains(_allTowerSight[i]) == true) //else
                {
                    if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
                    {

                        UpOrDownSoldierAccordingToTarget();
                        return;
                    }
                    if (_allTowerSight[i].IntersectsWith(soldier) == true)
                    {
                        //if (_allTowerSight[i].IntersectsWith(soldierOneStepBefore) == true)
                        //// if (_allTowerSight[i].IntersectsWith(soldierOneStepForward) == true)
                        //{
                        //    AvoidFromKnownTower(_allTowers[i]);
                        //    TowerHitsToSoldier();
                        //    return;
                        //}
                        AvoidFromKnownTower(_allTowers[i]);
                        return;
                    }
                    if (_allTowerSight[i].IntersectsWith(soldierSight) == true)
                    {  //kule  askerin görüş alanını görüyorsa      
                       //if (_allTowerSight[i].IntersectsWith(soldier) == true)
                       //{
                       //    _moveSoldierX += _soldierMaxMovement;//_defaultStep; 
                       //    StepSoldier(_moveSoldierX, _currentSoldier.Location.Y);
                       //    TowerHitsToSoldier();
                       //    return;

                        //}
                        if (_allTowerSight[i].IntersectsWith(soldier) == false)
                        {
                            if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
                            {

                                UpOrDownSoldierAccordingToTarget();
                                return;
                            }
                            _moveSoldierX += _soldierMaxMovement;
                            StepSoldier(_moveSoldierX, _currentSoldier.Location.Y);
                            return;
                        }

                    }
                }

            } //general tower

            MoveSoldier();
        }

        private void UpOrDownSoldierAccordingToTarget()
        {
            if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
            {
                _moveSoldierY = (_targetY - (_targetSize) > _currentSoldierY) ? _moveSoldierY += _soldierMaxMovement : (_targetY + (_targetSize) < _currentSoldierY) ? _moveSoldierY -= _soldierMaxMovement : _moveSoldierY;
                StepSoldier(_currentSoldier.Location.X, _moveSoldierY);
            }
        }

        private void MoveSoldier()
        {
            if (_currentSoldierX > _targetX - _unitSize && _currentSoldierY > _targetY - _unitSize && _currentSoldierY < _targetY + _unitSize)
            {
                mainTimer.Stop();
                _currentSoldierNumber++;

                if (_currentSoldierNumber < _numberOfSoldiers)
                {
                    _moveSoldierX = 0;
                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                    lblCurrentHealth.Text = _health.ToString();
                    _currentHealth = _health;
                    mainTimer.Start();
                }
                else
                {
                    _currentSoldierNumber--;
                    MessageBox.Show(@"Soldiers reached to target!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mainTimer.Stop();
                    lblCurrentHealth.Text = _health.ToString();
                    return;
                }
            }
            else
            {
                #region defaultStepValue
                if (_targetX < _currentSoldierX + _unitSize)
                {
                    if ((_targetY - (_targetSize) > _currentSoldierY))
                    {
                        _moveSoldierY += _defaultStep;
                        StepSoldier(_moveSoldierX, _moveSoldierY);
                    }
                    else if ((_targetY + (_targetSize) < _currentSoldierY))
                    {
                        _moveSoldierY -= _defaultStep;
                        StepSoldier(_moveSoldierX, _moveSoldierY);
                    }
                    return;
                }

                if (_isSoldierAlreadyGoneToX == false)
                {
                    if (_targetX >= _currentSoldierX + _unitSize)
                    {
                        _moveSoldierX += _defaultStep;
                        StepSoldier(_moveSoldierX, _moveSoldierY);
                        _isSoldierAlreadyGoneToX = true;
                    }
                }
                else if (_isSoldierAlreadyGoneToX == true)
                {
                    if ((_targetY - (_targetSize) > _currentSoldierY))
                    {
                        _moveSoldierY += _defaultStep;
                        StepSoldier(_moveSoldierX, _moveSoldierY);
                        _isSoldierAlreadyGoneToX = false;
                    }
                    else if ((_targetY + (_targetSize) < _currentSoldierY))
                    {
                        _moveSoldierY -= _defaultStep;
                        StepSoldier(_moveSoldierX, _moveSoldierY);
                        _isSoldierAlreadyGoneToX = false;
                    }
                    else
                    {
                        _moveSoldierX += _defaultStep;
                        StepSoldier(_moveSoldierX, _moveSoldierY);
                        return;
                    }
                }
                #endregion
            }
        }

        private void AvoidFromKnownTower(PictureBox attackerTower)
        {

            Graphics g = this.panelGodView.CreateGraphics();

            if (_currentSoldierX > _targetX - _unitSize && _currentSoldierY > _targetY - _unitSize && _currentSoldierY < _targetY + _unitSize)
            {
                mainTimer.Stop();
                _currentSoldierNumber++;

                if (_currentSoldierNumber < _numberOfSoldiers)
                {
                    _moveSoldierX = 0;
                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                    lblCurrentHealth.Text = _health.ToString();
                    _currentHealth = _health;
                    mainTimer.Start();
                }
                else
                {
                    _currentSoldierNumber--;
                    MessageBox.Show(@"Soldiers reached to target!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mainTimer.Stop();
                    lblCurrentHealth.Text = _health.ToString();
                    return;
                }
                return;
            }
            if (_currentSoldier.Top >= attackerTower.Top) //soldier towerdan aşağıda        
            {//target  daha aşağıda ise
                if (_currentSoldier.Location.Y + _unitSize >= _mainPanelSize)
                {
                    StepSoldier(_currentSoldierX + _soldierMaxMovement, _currentSoldier.Location.Y);
                }
                else
                {
                    StepSoldier(_currentSoldierX, _currentSoldierY + _soldierMaxMovement);
                }
                return;
            }
            else if (_currentSoldier.Top < attackerTower.Top) //soldier towerdan yukarıda
            {
                if (_currentSoldier.Location.Y <= 0)
                {
                    StepSoldier(_currentSoldierX + _soldierMaxMovement, _currentSoldier.Location.Y);
                }
                else
                {
                    StepSoldier(_currentSoldierX, _currentSoldierY - _soldierMaxMovement);
                }
                return;
            }

            if (_currentSoldier.Right >= attackerTower.Right) // soldiers solda
            {

                StepSoldier(_currentSoldierX - _soldierMaxMovement, _currentSoldierY);
            }
            else if (_currentSoldier.Right < attackerTower.Right) // soldier sağda
            {
                //if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
                //{
                //    _moveSoldierY = (_targetY - (_targetSize) > _currentSoldierY) ? _moveSoldierY += _soldierMaxMovement : (_targetY + (_targetSize) < _currentSoldierY) ? _moveSoldierY -= _soldierMaxMovement : _moveSoldierY;
                //    StepSoldier(_currentSoldier.Location.X, _moveSoldierY);
                //    return;
                //}

                StepSoldier(_currentSoldierX + _soldierMaxMovement, _currentSoldierY);
            }


        }

        private void CheckSoldier()
        {
            if (_currentSoldierX > _targetX - _unitSize && _currentSoldierY > _targetY - _unitSize && _currentSoldierY < _targetY + _unitSize)
            {
                mainTimer.Stop();
                _currentSoldierNumber++;

                if (_currentSoldierNumber < _numberOfSoldiers)
                {
                    _moveSoldierX = 0;
                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                    lblCurrentHealth.Text = _health.ToString();
                    _currentHealth = _health;
                    mainTimer.Start();
                }
                else
                {
                    _currentSoldierNumber--;
                    MessageBox.Show(@"Soldiers reached to target!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    mainTimer.Stop();
                    lblCurrentHealth.Text = _health.ToString();
                    return;
                }
            }

        }

        private void AvoidFromArcher(PictureBox attackerArcher)
        {
            Rectangle soldierOneStepBefore = new Rectangle(_currentSoldierX - _defaultStep, _currentSoldierY, _unitSize, _unitSize);
            Graphics g = this.panelGodView.CreateGraphics();
            if (_currentSoldier.Location.Y + _unitSize >= _mainPanelSize)
            {
                _moveSoldierX += _soldierMaxMovement; ;
                StepSoldier(_moveSoldierX, _allSoldiers[_currentSoldierNumber].Location.Y);
                return;
            }
            else if (_currentSoldier.Location.Y <= 0)//
            {
                _moveSoldierX += _soldierMaxMovement; ;
                StepSoldier(_moveSoldierX, _allSoldiers[_currentSoldierNumber].Location.Y);
                return;
            }

            if (_currentSoldierX > _targetX - _unitSize && _currentSoldierY > _targetY - _unitSize && _currentSoldierY < _targetY + _unitSize)
            {
                mainTimer.Stop();
                _currentSoldierNumber++;

                if (_currentSoldierNumber < _numberOfSoldiers)
                {
                    _moveSoldierX = 0;
                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                    lblCurrentHealth.Text = _health.ToString();
                    _currentHealth = _health;
                    mainTimer.Start();
                }
                else
                {
                    _currentSoldierNumber--;
                    MessageBox.Show(@"Soldiers reached to target!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblCurrentHealth.Text = _health.ToString();
                    return;
                }
                return;
            }
            if (_currentSoldier.Right >= attackerArcher.Right) // soldiers sağda        
            {
                if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
                {
                    UpOrDownSoldierAccordingToTarget();
                    return;
                }

                StepSoldier(_currentSoldierX + _soldierMaxMovement, _currentSoldierY);
            }
            else if (_currentSoldier.Right < attackerArcher.Right) // soldier solda     
            {
                StepSoldier(_currentSoldierX - _soldierMaxMovement, _currentSoldierY);
            }

        }

        private void TowerHitsToSoldier()
        {
            if (_currentHealth - _towerHitPower > 0)
            {
                _currentHealth -= _towerHitPower;
                lblCurrentHealth.Text = _currentHealth.ToString();
            }
            else
            {
                mainTimer.Stop();
                _allSoldiers[_currentSoldierNumber].Visible = false; // ölen asker gözükmeyecek
                _currentSoldierNumber++;
                _currentHealth = _health;

                if (_currentSoldierNumber < _numberOfSoldiers)
                {
                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                    _moveSoldierX = 0;
                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                    _currentSoldierX = _moveSoldierX;
                    _currentSoldierY = _moveSoldierY;

                    lblCurrentHealth.Text = _health.ToString();

                    mainTimer.Start();
                }
                else
                {
                    mainTimer.Stop();
                    _currentSoldierNumber--;
                    lblCurrentHealth.Text = "0";
                    MessageBox.Show(@"No soldier left!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

        }

        private void ArcherHitsToSoldier()
        {
            if (_currentHealth - _archerHitPower > 0)
            {
                _currentHealth -= _archerHitPower;
                lblCurrentHealth.Text = _currentHealth.ToString();
            }
            else
            {
                mainTimer.Stop();
                _allSoldiers[_currentSoldierNumber].Visible = false;
                _currentHealth = _health;
                _currentSoldierNumber++;

                if (_currentSoldierNumber < _numberOfSoldiers)
                {
                    _currentSoldier = _allSoldiers[_currentSoldierNumber];
                    _moveSoldierX = 0;
                    _moveSoldierY = _allSoldiers[_currentSoldierNumber].Location.Y;
                    _currentSoldierX = _moveSoldierX;
                    _currentSoldierY = _moveSoldierY;

                    lblCurrentHealth.Text = _health.ToString();

                    mainTimer.Start();
                }
                else
                {
                    mainTimer.Stop();
                    _currentSoldierNumber--;
                    lblCurrentHealth.Text = "0";
                    MessageBox.Show(@"No soldier left!" + Environment.NewLine + _knownTowerNumber.ToString() + " tower detected" + Environment.NewLine + _knownArcherNumber.ToString() + " archer detected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void StepSoldier(int x, int y)
        {
            this._currentSoldier.Location = new Point(x, y);
            soldierEyePictureBox.Location = new Point(Convert.ToInt32((Convert.ToDouble(x) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))), Convert.ToInt32((Convert.ToDouble(y) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))));
            CheckSoldier();
        }

        private void ArcherMoveMaxStep(PictureBox archer, PictureBox soldier)
        {
            if (soldier.Top >= archer.Top) //soldier daha aşağıdaysa
            {
                ArcherGoDownMaxStep(archer);
            }
            else if (soldier.Top < archer.Top)
            {
                ArcherGoUpMaxStep(archer);
            }
        }

        private void ArcherGoUpMaxStep(PictureBox archer)
        {
            //if (archer.Bottom - _archerMaxMovement <= 0)
            //{
            //    archer.Location = new Point(archer.Location.X, archer.Location.Y + _archerMaxMovement);
            //}
            //else
            //{
            archer.Location = new Point(archer.Location.X, archer.Location.Y - _archerMaxMovement);
            //}
        }

        private void ArcherGoDownMaxStep(PictureBox archer)
        {
            //if (archer.Top - _archerMaxMovement <= 0)
            //{
            //    archer.Location = new Point(archer.Location.X, archer.Location.Y - _archerMaxMovement);
            //}
            //else
            //{
            archer.Location = new Point(archer.Location.X, archer.Location.Y + _archerMaxMovement);

            // }

        }

        private void ArcherGoUpDefaultStep(PictureBox archer)
        {
            archer.Location = new Point(archer.Location.X, archer.Location.Y - _defaultStep);
        }
        private void ArcherGoDownDefaultStep(PictureBox archer)
        {
            archer.Location = new Point(archer.Location.X, archer.Location.Y + _defaultStep);
        }

        #region Variables
        Random _randomSoldierY = new Random();
        Random _randomTowerY = new Random();
        Random _randomTowerX = new Random();
        Random _randomArcherY = new Random();
        Random _randomArcherX = new Random();
        Random _randomTargetY = new Random();
        Random _archerRandomMoveDirection = new Random();

        private int[] _archerDirection;
        private int[] _archerInitialDirection;

        PictureBox _currentSoldier = new PictureBox();
        PictureBox[] _allSoldiers;
        PictureBox[] _allArchers;
        PictureBox[] _allTowers;
        PictureBox[] _archerForSoldierEye;

        Rectangle[] _allTowerSight;
        Rectangle[] _allSoldierSight;
        Rectangle[] _allArcherSightRange;
        Rectangle[] _allArcherHitRange;
        Rectangle[] _allObstacles;

        Pen _redPen = new Pen(Color.Red);
        Pen _greenPen = new Pen(Color.Green);
        Pen _blackPen = new Pen(Color.Black);
        Pen _bluePen = new Pen(Color.Blue);

        private int _numberOfSoldiers;
        private int _numberOfTowers;
        private int _numberOfArcher;
        private int _soldierMaxMovement;
        private int _archerMaxMovement;

        private int _mainPanelSize;
        private int _soldiersEyePanelSize = 300;
        private int _unitSize;
        private int _targetSize;
        private int _soldierEyeTargetSize;
        private int _soldierEyeUnitSize;

        private int _towerSight;
        private int _soldierSight;
        private int _archerSightRange;
        private int _archerHitRange;

        private int _health;
        private int _towerHitPower;
        private int _archerHitPower;
        private int _minDistanceFromTower;
        private int _defaultStep;

        private int _targetX;
        private int _targetY;

        Point _scenarioTargetLocation;
        Point[] _scenarioSoldiers;
        Point[] _scenarioArchers;
        Point[] _scenarioTowers;
        private int[] _scenarioUserVariables;
        private int[] _scenarioArcherFirstDirection;
        #endregion

        private void SetFirstLocation()
        {
            try
            {


                int[] scnenarioUserVariables = new int[14];

                _mainPanelSize = tboxPanelSize.Text == null || tboxPanelSize.Text == string.Empty ? _mainPanelSize = 700 : _mainPanelSize = Convert.ToInt32(tboxPanelSize.Text);
                scnenarioUserVariables[0] = _mainPanelSize;
                _archerSightRange = tboxArcherSight.Text == null || tboxArcherSight.Text == string.Empty ? _archerSightRange = 70 : _archerSightRange = Convert.ToInt32(tboxArcherSight.Text);
                scnenarioUserVariables[1] = _archerSightRange;
                _archerHitRange = tboxHitRangeOfArcher.Text == null || tboxHitRangeOfArcher.Text == string.Empty ? _archerHitRange = 40 : _archerHitRange = Convert.ToInt32(tboxHitRangeOfArcher.Text);
                scnenarioUserVariables[2] = _archerHitRange;
                int defaultStep = _mainPanelSize / 70;
                _defaultStep = defaultStep;
                if (_mainPanelSize >= 300 && _mainPanelSize <= 700 && _archerSightRange >= _archerHitRange)
                {
                    Graphics graphicsGodPanel = this.panelGodView.CreateGraphics();

                    panelGodView.Height = _mainPanelSize;
                    panelGodView.Width = _mainPanelSize;

                    RefreshGame();
                    panelTarget.Visible = true;
                    panelTargetSoldiersEye.Visible = true;
                    soldierEyePictureBox.Visible = true;


                    #region userVariables
                    _soldierMaxMovement = tboxSoldierMaxMovement.Text == null || tboxSoldierMaxMovement.Text == string.Empty ? _soldierMaxMovement = _defaultStep : _soldierMaxMovement = Convert.ToInt32(tboxSoldierMaxMovement.Text);
                    scnenarioUserVariables[3] = _soldierMaxMovement;

                    _archerMaxMovement = tboxMaxMovementOfArcher.Text == null || tboxMaxMovementOfArcher.Text == string.Empty ? _archerMaxMovement = _defaultStep : _archerMaxMovement = Convert.ToInt32(tboxMaxMovementOfArcher.Text);
                    scnenarioUserVariables[4] = _archerMaxMovement;

                    _numberOfSoldiers = tboxNumberOfSoldiers.Text == null || tboxNumberOfSoldiers.Text == string.Empty ? _numberOfSoldiers = 1 : _numberOfSoldiers = Convert.ToInt32(tboxNumberOfSoldiers.Text);
                    scnenarioUserVariables[5] = _numberOfSoldiers;

                    _numberOfTowers = tboxNumberOfTowers.Text == null || tboxNumberOfTowers.Text == string.Empty ? _numberOfTowers = 1 : _numberOfTowers = Convert.ToInt32(tboxNumberOfTowers.Text);
                    scnenarioUserVariables[6] = _numberOfTowers;

                    _towerSight = tboxSightRangeOfTowers.Text == null || tboxSightRangeOfTowers.Text == string.Empty ? _towerSight = 35 : _towerSight = Convert.ToInt32(tboxSightRangeOfTowers.Text);
                    scnenarioUserVariables[7] = _towerSight;

                    _soldierSight = tboxSightOfSoldier.Text == null || tboxSightOfSoldier.Text == string.Empty ? _soldierSight = 35 : _soldierSight = Convert.ToInt32(tboxSightOfSoldier.Text);
                    scnenarioUserVariables[8] = _soldierSight;

                    _health = tboxHealthOfSoldier.Text == null || tboxHealthOfSoldier.Text == string.Empty ? _health = 100 : _health = Convert.ToInt32(tboxHealthOfSoldier.Text);
                    scnenarioUserVariables[9] = _health;

                    _towerHitPower = tboxHitPowerOfTower.Text == null || tboxHitPowerOfTower.Text == string.Empty ? _towerHitPower = 1 : _towerHitPower = Convert.ToInt32(tboxHitPowerOfTower.Text);
                    scnenarioUserVariables[10] = _towerHitPower;

                    _minDistanceFromTower = tboxMinDistanceFromTarget.Text == null || tboxMinDistanceFromTarget.Text == string.Empty ? _minDistanceFromTower = 0 : _minDistanceFromTower = Convert.ToInt32(tboxMinDistanceFromTarget.Text);
                    scnenarioUserVariables[11] = _minDistanceFromTower;

                    _numberOfArcher = tboxNumberOfArchers.Text == null || tboxNumberOfArchers.Text == string.Empty ? _numberOfArcher = 1 : _numberOfArcher = Convert.ToInt32(tboxNumberOfArchers.Text);
                    scnenarioUserVariables[12] = _numberOfArcher;

                    _archerHitPower = tboxHitPowerOfArcher.Text == null || tboxHitPowerOfArcher.Text == string.Empty ? _archerHitPower = 1 : _archerHitPower = Convert.ToInt32(tboxHitPowerOfArcher.Text);
                    scnenarioUserVariables[13] = _archerHitPower;
                    _scenarioUserVariables = scnenarioUserVariables;

                    tboxPanelSize.Text = _mainPanelSize.ToString();
                    tboxArcherSight.Text = _archerSightRange.ToString();
                    tboxHitRangeOfArcher.Text = _archerHitRange.ToString();
                    tboxSoldierMaxMovement.Text = _soldierMaxMovement.ToString();
                    tboxMaxMovementOfArcher.Text = _archerMaxMovement.ToString();
                    tboxNumberOfSoldiers.Text = _numberOfSoldiers.ToString();
                    tboxNumberOfTowers.Text = _numberOfTowers.ToString();
                    tboxSightRangeOfTowers.Text = _towerSight.ToString();
                    tboxSightOfSoldier.Text = _soldierSight.ToString();
                    tboxHealthOfSoldier.Text = _health.ToString();
                    tboxHitPowerOfTower.Text = _towerHitPower.ToString();
                    tboxMinDistanceFromTarget.Text = _minDistanceFromTower.ToString();
                    tboxNumberOfArchers.Text = _numberOfArcher.ToString();
                    tboxHitPowerOfArcher.Text = _archerHitPower.ToString();
                    #endregion

                    _currentHealth = _health;
                    lblCurrentHealth.Text = _currentHealth.ToString();
                    _unitSize = _mainPanelSize / 20;
                    _targetSize = _mainPanelSize / 28;
                    _soldierEyeTargetSize = Convert.ToInt32(_targetSize / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)));
                    _soldierEyeUnitSize = Convert.ToInt32(_unitSize / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)));


                    #region Target located
                    var targetRandomY = _randomTargetY.Next(_targetSize, _mainPanelSize - _targetSize);
                    this.panelTarget.Location = new Point(_mainPanelSize - _targetSize, targetRandomY); //target location set 
                    this.panelTargetSoldiersEye.Location = new Point(_soldiersEyePanelSize - _soldierEyeTargetSize, Convert.ToInt32((Convert.ToDouble(targetRandomY) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))));
                    panelTargetSoldiersEye.Tag = "panelTargetSoldiersEye";

                    panelTarget.Height = _targetSize;
                    panelTarget.Width = _targetSize;

                    panelTargetSoldiersEye.Height = _soldierEyeTargetSize;
                    panelTargetSoldiersEye.Width = _soldierEyeTargetSize;
                    soldierEyePictureBox.Height = _soldierEyeUnitSize;
                    soldierEyePictureBox.Width = _soldierEyeUnitSize;
                    int targetX = panelTarget.Location.X;
                    int targetY = panelTarget.Location.Y;
                    _targetX = targetX;
                    _targetY = targetY;
                    _scenarioTargetLocation = new Point(_targetX, _targetY);

                    #endregion


                    #region towers created and locations set
                    PictureBox[] tower = new PictureBox[_numberOfTowers];
                    Rectangle[] towerSight = new Rectangle[_numberOfTowers];
                    Rectangle[] listOfKnownTowers = new Rectangle[_numberOfTowers];
                    Point[] scenarioTowers = new Point[_numberOfTowers];


                    for (int i = 0; i < _numberOfTowers; i++)
                    {
                        int randomTowerY = _randomTowerY.Next(_mainPanelSize / 10, _mainPanelSize - (_unitSize * 2));
                        int randomTowerX = randomTowerY > _targetY + _minDistanceFromTower || randomTowerY < Math.Abs(_targetY - _minDistanceFromTower) ? _randomTowerX.Next(_mainPanelSize / 10, (_mainPanelSize - (_unitSize * 2))) : _randomTowerX.Next(_mainPanelSize / 10, (_mainPanelSize - (_unitSize * 2)) - _minDistanceFromTower);

                        tower[i] = new PictureBox();
                        tower[i].Location = new Point(randomTowerX, randomTowerY);
                        tower[i].Size = new Size(_unitSize, _unitSize);
                        tower[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        tower[i].Visible = true;
                        tower[i].Image = PathPlanning.Properties.Resources.tower;
                        panelGodView.Controls.Add(tower[i]);
                        scenarioTowers[i] = tower[i].Location;

                        int towerSightX = tower[i].Location.X - ((_towerSight * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        int towerSightY = tower[i].Location.Y - ((_towerSight * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        towerSight[i] = new Rectangle(new Point(towerSightX, towerSightY), new Size((_towerSight * 2) / (700 / _mainPanelSize), (_towerSight * 2) / (700 / _mainPanelSize)));

                    }

                    _listOfKnownTowers = listOfKnownTowers;
                    _allTowers = tower;
                    _allTowerSight = towerSight;
                    _scenarioTowers = scenarioTowers;
                    #endregion



                    #region archers created and first locations set
                    PictureBox[] archer = new PictureBox[_numberOfArcher];
                    Rectangle[] archerSightRange = new Rectangle[_numberOfArcher];
                    Rectangle[] archerHitRange = new Rectangle[_numberOfArcher];
                    //Rectangle[] listOfKnownArchers = new Rectangle[_numberOfArcher];
                    PictureBox[] listOfKnownArchers = new PictureBox[_numberOfArcher];
                    Point[] scenarioArcher = new Point[_numberOfArcher];
                    int[] archerDirection = new int[_numberOfArcher];
                    int[] archerInitialDirection = new int[_numberOfArcher];

                    for (int i = 0; i < _numberOfArcher; i++)
                    {
                        int randomArcherY = _randomArcherY.Next(_mainPanelSize / 10, _mainPanelSize - (_unitSize * 2));
                        int randomArcherX = randomArcherY > _targetY + _minDistanceFromTower || randomArcherY < Math.Abs(_targetY - _minDistanceFromTower) ? _randomArcherX.Next(_mainPanelSize / 10, (_mainPanelSize - (_unitSize * 2))) : _randomArcherX.Next(_mainPanelSize / 10, (_mainPanelSize - (_unitSize * 2)) - _minDistanceFromTower);

                        archer[i] = new PictureBox();
                        archer[i].Location = new Point(randomArcherX, randomArcherY);
                        archer[i].Size = new Size(_unitSize, _unitSize);
                        archer[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        archer[i].Visible = true;
                        archer[i].Image = PathPlanning.Properties.Resources.archer;
                        panelGodView.Controls.Add(archer[i]);
                        scenarioArcher[i] = archer[i].Location;

                        archerSightRange[i] = new Rectangle();
                        int archerSightX = archer[i].Location.X - ((_archerSightRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        int archerSightY = archer[i].Location.Y - ((_archerSightRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        archerSightRange[i].Location = new Point(archerSightX, archerSightY);
                        archerSightRange[i].Size = new Size((_archerSightRange * 2) / (700 / _mainPanelSize), (_archerSightRange * 2) / (700 / _mainPanelSize));

                        archerHitRange[i] = new Rectangle();
                        int archerHitX = archer[i].Location.X - ((_archerHitRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        int archerHitY = archer[i].Location.Y - ((_archerHitRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        archerHitRange[i].Location = new Point(archerHitX, archerHitY);
                        archerHitRange[i].Size = new Size((_archerHitRange * 2) / (700 / _mainPanelSize), (_archerHitRange * 2) / (700 / _mainPanelSize));

                        int x = _archerRandomMoveDirection.Next(0, 10);
                        archerDirection[i] = x < 5 ? archerDirection[i] = 1 : archerDirection[i] = 0;
                        archerInitialDirection[i] = archerDirection[i];
                    }

                    _allArchers = archer;
                    _allArcherSightRange = archerSightRange;
                    _allArcherHitRange = archerHitRange;

                    _listOfKnownArchers = listOfKnownArchers;
                    _archerForSoldierEye = listOfKnownArchers;

                    _scenarioArchers = scenarioArcher;
                    _archerDirection = archerDirection;

                    _archerInitialDirection = archerInitialDirection;
                    _scenarioArcherFirstDirection = _archerInitialDirection;

                    #endregion



                    #region soldiers created and locations set
                    PictureBox[] soldier = new PictureBox[_numberOfSoldiers];
                    Point[] scneraioSoldier = new Point[_numberOfSoldiers];
                    Rectangle[] soldierSight = new Rectangle[_numberOfSoldiers];

                    for (int i = 0; i < _numberOfSoldiers; i++)
                    {
                        var randomSoldierY = _randomSoldierY.Next(0, _mainPanelSize - _unitSize);
                        soldier[i] = new PictureBox();
                        soldier[i].Size = new Size(_unitSize, _unitSize);
                        soldier[i].Location = new Point(0, randomSoldierY);
                        soldier[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        soldier[i].Image = PathPlanning.Properties.Resources.soldier;
                        scneraioSoldier[i] = soldier[i].Location;
                        panelGodView.Controls.Add(soldier[i]);

                        soldierSight[i] = new Rectangle();
                        int soldierSightX = soldier[i].Location.X - (((_soldierSight * 2) / (700 / _mainPanelSize)) - _unitSize) / 2;
                        int soldierSightY = soldier[i].Location.Y - (((_soldierSight * 2) / (700 / _mainPanelSize)) - _unitSize) / 2;
                        soldierSight[i].Location = new Point(soldierSightX, soldierSightY);
                        soldierSight[i].Size = new Size((_soldierSight * 2) / (700 / _mainPanelSize), (_soldierSight * 2) / (700 / _mainPanelSize));
                    }

                    //_currentSoldierSight = soldierSight[0];
                    _allSoldierSight = soldierSight;

                    _allSoldiers = soldier;
                    _currentSoldier = soldier[0];
                    _scenarioSoldiers = scneraioSoldier;
                    soldierEyePictureBox.Location = new Point(soldier[0].Location.X, Convert.ToInt32((Convert.ToDouble(soldier[0].Location.Y) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))));
                    soldierEyePictureBox.Tag = "soldierEyePictureBox";
                    #endregion




                    Rectangle[] listOfAllObtacles = new Rectangle[_numberOfArcher + _numberOfTowers];
                    _listOfAllObstacles = listOfAllObtacles;
                    _allObstacles = new Rectangle[_numberOfTowers + _numberOfArcher];
                    _allArcherSightRange.CopyTo(_allObstacles, 0);
                    _allTowerSight.CopyTo(_allObstacles, _numberOfArcher);

                    this.Invalidate();
                }
                else
                {
                    MessageBox.Show("Wrong Input!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _archerHitRange = 35;
                    _archerSightRange = 70;
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = this.panelGodView.CreateGraphics();

            Rectangle[] towerSight = new Rectangle[_numberOfTowers];

            for (int i = 0; i < _numberOfTowers; i++)
            {

                int towerSightX = _allTowers[i].Location.X - ((_towerSight * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                int towerSightY = _allTowers[i].Location.Y - ((_towerSight * 2) / (700 / _mainPanelSize) - _unitSize) / 2;

                g.DrawEllipse(_redPen, towerSightX, towerSightY, (_towerSight * 2) / (700 / _mainPanelSize), (_towerSight * 2) / (700 / _mainPanelSize));
            }

            for (int i = 0; i < _numberOfArcher; i++)
            {
                int archerSightX = _allArchers[i].Location.X - ((_archerSightRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2; ;
                int archerSightY = _allArchers[i].Location.Y - ((_archerSightRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2; ;
                _allArcherSightRange[i] = new Rectangle(archerSightX, archerSightY, (_archerSightRange * 2) / (700 / _mainPanelSize), (_archerSightRange * 2) / (700 / _mainPanelSize));
                g.DrawEllipse(_bluePen, _allArcherSightRange[i]);

                int archerHitX = _allArchers[i].Location.X - ((_archerHitRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                int archerHitY = _allArchers[i].Location.Y - ((_archerHitRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                _allArcherHitRange[i] = new Rectangle(archerHitX, archerHitY, (_archerHitRange * 2) / (700 / _mainPanelSize), (_archerHitRange * 2) / (700 / _mainPanelSize));
                g.DrawEllipse(_blackPen, _allArcherHitRange[i]);

            }

            for (int i = 0; i < _numberOfSoldiers; i++)
            {
                int soldierSightX = _allSoldiers[_currentSoldierNumber].Location.X - (((_soldierSight * 2) / (700 / _mainPanelSize)) - _unitSize) / 2; //repaint the rectangle of soldier in each move
                int soldierSightY = _allSoldiers[_currentSoldierNumber].Location.Y - (((_soldierSight * 2) / (700 / _mainPanelSize)) - _unitSize) / 2;
                _allSoldierSight[_currentSoldierNumber] = new Rectangle(soldierSightX, soldierSightY, (_soldierSight * 2) / (700 / _mainPanelSize), (_soldierSight * 2) / (700 / _mainPanelSize));
                g.DrawEllipse(_greenPen, _allSoldierSight[_currentSoldierNumber]);

            }


        }

        private void RefreshGame()
        {
            _moveSoldierX = 0;
            _moveSoldierY = 0;
            _currentSoldierNumber = 0;
            for (int i = 0; i < _numberOfSoldiers; i++)
            {
                _allSoldiers[i].Dispose();
            }
            for (int i = 0; i < _numberOfTowers; i++)
            {
                _allTowers[i].Dispose();
            }
            for (int i = 0; i < _numberOfArcher; i++)
            {
                _allArchers[i].Dispose();
            }

            panelSoldiersEye.Controls.Clear();
            panelSoldiersEye.Controls.Add(panelTargetSoldiersEye);
            panelSoldiersEye.Controls.Add(soldierEyePictureBox);

            mainTimer.Stop();
            panelTarget.Visible = false;
            panelTargetSoldiersEye.Visible = false;
            panelGodView.Invalidate(); //removes circles
            _currentHealth = _health;
            lblCurrentHealth.Text = String.Empty;
            //_allArchers = new PictureBox[];
            _knownTowerNumber = 0;
            _knownArcherNumber = 0;
            _knownObstacleNumber = 0;
            _soldierStuckNumber = 0;
        }

        private void btnSetUnits_Click_1(object sender, EventArgs e)
        {
            SetFirstLocation();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void btnStartAttack_Click(object sender, EventArgs e)
        {
            if (_allSoldierSight != null)
            {
                mainTimer.Start();
            }
        }

        private void btnStopAttack_Click(object sender, EventArgs e)
        {
            mainTimer.Stop();
        }

        #region StringBuilders
        StringBuilder _scenarioUserVariablesBuilder = new StringBuilder();
        StringBuilder _scenarioSoldiersBuilder = new StringBuilder();
        StringBuilder _scenarioArchersBuilder = new StringBuilder();
        StringBuilder _scenarioTowersBuilder = new StringBuilder();
        StringBuilder _scenarioArcherFirstDirectionBulder = new StringBuilder();

        #endregion

        private void btnSaveScenario_Click(object sender, EventArgs e)
        {
            _scenarioUserVariablesBuilder = new StringBuilder();
            _scenarioSoldiersBuilder = new StringBuilder();
            _scenarioArchersBuilder = new StringBuilder();
            _scenarioTowersBuilder = new StringBuilder();
            _scenarioArcherFirstDirectionBulder = new StringBuilder();

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
                        foreach (var item in _scenarioUserVariables)
                        {
                            _scenarioUserVariablesBuilder.Append(Convert.ToString(item.ToString()) + " ");
                        }

                        foreach (var item in _scenarioSoldiers)
                        {
                            _scenarioSoldiersBuilder.Append(item.X.ToString() + "," + item.Y.ToString() + ";");
                        }
                        foreach (var item in _scenarioArchers)
                        {
                            _scenarioArchersBuilder.Append(item.X.ToString() + "," + item.Y.ToString() + ";");
                        }
                        foreach (var item in _scenarioTowers)
                        {
                            _scenarioTowersBuilder.Append(item.X.ToString() + "," + item.Y.ToString() + ";");
                        }
                        foreach (var item in _scenarioArcherFirstDirection)
                        {
                            _scenarioArcherFirstDirectionBulder.Append(item.ToString() + " ");

                        }
                        streamWriter.WriteLine(_scenarioUserVariablesBuilder.ToString());
                        streamWriter.WriteLine(_scenarioTargetLocation.X.ToString() + "," + _scenarioTargetLocation.Y.ToString() + ";");
                        streamWriter.WriteLine(_scenarioSoldiersBuilder.ToString());
                        streamWriter.WriteLine(_scenarioArchersBuilder.ToString());
                        streamWriter.WriteLine(_scenarioTowersBuilder.ToString());
                        streamWriter.WriteLine(_scenarioArcherFirstDirectionBulder.ToString());


                        MessageBox.Show("Succesfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnOpenScenario_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"C:\txt";
            openFile.Filter = "Text Files Only (*.txt) | *.txt";
            openFile.DefaultExt = "txt";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                StringBuilder stringBuilder = new StringBuilder();
                _scenarioUserVariables = new int[14];


                using (StreamReader reader = new StreamReader(openFile.OpenFile()))
                {
                    string userVariables = reader.ReadLine();
                    string[] userVariablesSplitted = userVariables.Split(' ').ToArray();
                    for (int i = 0; i < 14; i++)
                    {
                        _scenarioUserVariables[i] = Convert.ToInt32(userVariablesSplitted[i]);
                    }

                    string targetLoc = reader.ReadLine();
                    if (targetLoc.Length > 0)
                    {
                        var targetSplitted = targetLoc.Substring(0, targetLoc.Length - 1).Split(';').Select(x => x.Split(',')).Select(y => new Point(Convert.ToInt32(y[0]), Convert.ToInt32(y[1]))).ToArray();
                        _scenarioTargetLocation = targetSplitted[0];
                    }

                    string soldierLocations = reader.ReadLine();
                    if (soldierLocations.Length > 0)
                    {
                        var soldierLocationsSplitted = soldierLocations.Substring(0, soldierLocations.Length - 1).Split(';').Select(y => y.Split(',')).Select(y => new Point(int.Parse(y[0]), Convert.ToInt32(y[1]))).ToArray();
                        _scenarioSoldiers = soldierLocationsSplitted;
                        for (int i = 0; i < soldierLocationsSplitted.Length; i++)
                        {
                            _scenarioSoldiers[i] = soldierLocationsSplitted[i];
                        }
                    }


                    string archerLocations = reader.ReadLine();
                    if (archerLocations.Length > 0)
                    {
                        var archerLocationsSplitted = archerLocations.Substring(0, archerLocations.Length - 1).Split(';').Select(y => y.Split(',')).Select(y => new Point(Convert.ToInt32(y[0]), Convert.ToInt32(y[1]))).ToArray();
                        _scenarioArchers = archerLocationsSplitted;
                        for (int i = 0; i < archerLocationsSplitted.Length; i++)
                        {
                            _scenarioArchers[i] = archerLocationsSplitted[i];
                        }
                    }

                    string towerLocations = reader.ReadLine();
                    if (towerLocations.Length > 0)
                    {
                        var towerLocationsSplitted = towerLocations.Substring(0, towerLocations.Length - 1).Split(';').Select(y => y.Split(',')).Select(y => new Point(Convert.ToInt32(y[0]), Convert.ToInt32(y[1]))).ToArray();
                        _scenarioTowers = towerLocationsSplitted;
                        for (int i = 0; i < towerLocationsSplitted.Length; i++)
                        {
                            _scenarioTowers[i] = towerLocationsSplitted[i];
                        }
                    }


                    string archerDirection = reader.ReadLine();
                    if (archerDirection.Length > 0)
                    {
                        string[] archerDirectionSplitted = archerDirection.Substring(0, archerDirection.Length - 1).Split(' ').ToArray();
                        _scenarioArcherFirstDirection = new int[archerDirectionSplitted.Length];
                        for (int i = 0; i < archerDirectionSplitted.Length; i++)
                        {
                            _scenarioArcherFirstDirection[i] = Convert.ToInt32(archerDirectionSplitted[i]);
                        }
                    }

                }


                _mainPanelSize = _scenarioUserVariables[0];
                tboxPanelSize.Text = _mainPanelSize.ToString();
                _archerSightRange = _scenarioUserVariables[1];
                tboxArcherSight.Text = _archerSightRange.ToString();
                _archerHitRange = _scenarioUserVariables[2];
                tboxHitRangeOfArcher.Text = _archerHitRange.ToString();
                int defaultStep = _mainPanelSize / 70;
                _defaultStep = defaultStep;

                if (_mainPanelSize >= 300 && _mainPanelSize <= 700 && _archerSightRange >= _archerHitRange)
                {
                    Graphics graphicsGodPanel = this.panelGodView.CreateGraphics();

                    panelGodView.Height = _mainPanelSize;
                    panelGodView.Width = _mainPanelSize;

                    RefreshGame();
                    panelTarget.Visible = true;
                    panelTargetSoldiersEye.Visible = true;
                    soldierEyePictureBox.Visible = true;

                    #region UserVariables
                    _soldierMaxMovement = _scenarioUserVariables[3];
                    tboxSoldierMaxMovement.Text = _soldierMaxMovement.ToString();
                    _archerMaxMovement = _scenarioUserVariables[4];
                    tboxMaxMovementOfArcher.Text = _archerMaxMovement.ToString();
                    _numberOfSoldiers = _scenarioUserVariables[5];
                    tboxNumberOfSoldiers.Text = _numberOfSoldiers.ToString();
                    _numberOfTowers = _scenarioUserVariables[6];
                    tboxNumberOfTowers.Text = _numberOfTowers.ToString();
                    _towerSight = _scenarioUserVariables[7];
                    tboxSightRangeOfTowers.Text = _towerSight.ToString();
                    _soldierSight = _scenarioUserVariables[8];
                    tboxSightOfSoldier.Text = _soldierSight.ToString();
                    _health = _scenarioUserVariables[9];
                    tboxHealthOfSoldier.Text = _health.ToString();
                    _towerHitPower = _scenarioUserVariables[10];
                    tboxHitPowerOfTower.Text = _towerHitPower.ToString();
                    _minDistanceFromTower = _scenarioUserVariables[11];
                    tboxMinDistanceFromTarget.Text = _minDistanceFromTower.ToString();
                    _numberOfArcher = _scenarioUserVariables[12];
                    tboxNumberOfArchers.Text = _numberOfArcher.ToString();
                    _archerHitPower = _scenarioUserVariables[13];
                    tboxHitPowerOfArcher.Text = _archerHitPower.ToString();
                    #endregion


                    _currentHealth = _health;
                    lblCurrentHealth.Text = _currentHealth.ToString();
                    _unitSize = _mainPanelSize / 20;
                    _targetSize = _mainPanelSize / 28;
                    _soldierEyeTargetSize = Convert.ToInt32(_targetSize / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)));
                    _soldierEyeUnitSize = Convert.ToInt32(_unitSize / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)));


                    #region Target located
                    this.panelTarget.Location = _scenarioTargetLocation;

                    this.panelTargetSoldiersEye.Location = new Point(_soldiersEyePanelSize - _soldierEyeTargetSize, Convert.ToInt32((Convert.ToDouble(_scenarioTargetLocation.Y) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))));
                    panelTargetSoldiersEye.Tag = "panelTargetSoldiersEye";

                    panelTarget.Height = _targetSize;
                    panelTarget.Width = _targetSize;

                    panelTargetSoldiersEye.Height = _soldierEyeTargetSize;
                    panelTargetSoldiersEye.Width = _soldierEyeTargetSize;
                    soldierEyePictureBox.Height = _soldierEyeUnitSize;
                    soldierEyePictureBox.Width = _soldierEyeUnitSize;
                    int targetX = panelTarget.Location.X;
                    int targetY = panelTarget.Location.Y;
                    _targetX = targetX;
                    _targetY = targetY;

                    #endregion


                    #region towers created and locations set
                    PictureBox[] tower = new PictureBox[_numberOfTowers];
                    Rectangle[] towerSight = new Rectangle[_numberOfTowers];
                    Rectangle[] listOfKnownTowers = new Rectangle[_numberOfTowers];


                    for (int i = 0; i < _numberOfTowers; i++)
                    {
                        int randomTowerY = _randomTowerY.Next(_mainPanelSize / 10, _mainPanelSize - (_unitSize * 2));
                        int randomTowerX = randomTowerY > _targetY + _minDistanceFromTower || randomTowerY < Math.Abs(_targetY - _minDistanceFromTower) ? _randomTowerX.Next(_mainPanelSize / 10, (_mainPanelSize - (_unitSize * 2))) : _randomTowerX.Next(_mainPanelSize / 10, (_mainPanelSize - (_unitSize * 2)) - _minDistanceFromTower);

                        tower[i] = new PictureBox();
                        tower[i].Location = _scenarioTowers[i];//new Point(randomTowerX, randomTowerY);
                        tower[i].Size = new Size(_unitSize, _unitSize);
                        tower[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        tower[i].Visible = true;
                        tower[i].Image = PathPlanning.Properties.Resources.tower;
                        panelGodView.Controls.Add(tower[i]);


                        int towerSightX = tower[i].Location.X - ((_towerSight * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        int towerSightY = tower[i].Location.Y - ((_towerSight * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        towerSight[i] = new Rectangle(new Point(towerSightX, towerSightY), new Size((_towerSight * 2) / (700 / _mainPanelSize), (_towerSight * 2) / (700 / _mainPanelSize)));

                    }

                    _listOfKnownTowers = listOfKnownTowers;
                    _allTowers = tower;
                    _allTowerSight = towerSight;

                    #endregion



                    #region archers created and first locations set
                    PictureBox[] archer = new PictureBox[_numberOfArcher];
                    Rectangle[] archerSightRange = new Rectangle[_numberOfArcher];
                    Rectangle[] archerHitRange = new Rectangle[_numberOfArcher];
                    PictureBox[] archerForSoldierEye = new PictureBox[_numberOfArcher];

                    //Rectangle[] listOfKnownArchers = new Rectangle[_numberOfArcher];
                    PictureBox[] listOfKnownArchers = new PictureBox[_numberOfArcher];
                    int[] archerFirstDirection = new int[_numberOfArcher];
                    for (int i = 0; i < _numberOfArcher; i++)
                    {
                        int randomArcherY = _randomArcherY.Next(_mainPanelSize / 10, _mainPanelSize - (_unitSize * 2));
                        int randomArcherX = randomArcherY > _targetY + _minDistanceFromTower || randomArcherY < Math.Abs(_targetY - _minDistanceFromTower) ? _randomArcherX.Next(_mainPanelSize / 10, (_mainPanelSize - (_unitSize * 2))) : _randomArcherX.Next(_mainPanelSize / 10, (_mainPanelSize - (_unitSize * 2)) - _minDistanceFromTower);

                        archer[i] = new PictureBox();
                        archer[i].Location = _scenarioArchers[i];//new Point(randomArcherX, randomArcherY);
                        archer[i].Size = new Size(_unitSize, _unitSize);
                        archer[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        archer[i].Visible = true;
                        archer[i].Image = PathPlanning.Properties.Resources.archer;
                        panelGodView.Controls.Add(archer[i]);

                        archerSightRange[i] = new Rectangle();
                        int archerSightX = archer[i].Location.X - ((_archerSightRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        int archerSightY = archer[i].Location.Y - ((_archerSightRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        archerSightRange[i].Location = new Point(archerSightX, archerSightY);
                        archerSightRange[i].Size = new Size((_archerSightRange * 2) / (700 / _mainPanelSize), (_archerSightRange * 2) / (700 / _mainPanelSize));

                        archerHitRange[i] = new Rectangle();
                        int archerHitX = archer[i].Location.X - ((_archerHitRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        int archerHitY = archer[i].Location.Y - ((_archerHitRange * 2) / (700 / _mainPanelSize) - _unitSize) / 2;
                        archerHitRange[i].Location = new Point(archerHitX, archerHitY);
                        archerHitRange[i].Size = new Size((_archerHitRange * 2) / (700 / _mainPanelSize), (_archerHitRange * 2) / (700 / _mainPanelSize));
                        archerFirstDirection[i] = _scenarioArcherFirstDirection[i];

                    }

                    _allArchers = archer;
                    _allArcherSightRange = archerSightRange;
                    _allArcherHitRange = archerHitRange;
                    _listOfKnownArchers = listOfKnownArchers;
                    _archerForSoldierEye = archerForSoldierEye;
                    _archerDirection = archerFirstDirection;

                    #endregion



                    #region soldiers created and locations set
                    PictureBox[] soldier = new PictureBox[_numberOfSoldiers];
                    Rectangle[] soldierSight = new Rectangle[_numberOfSoldiers];

                    for (int i = 0; i < _numberOfSoldiers; i++)
                    {
                        var randomSoldierY = _randomSoldierY.Next(0, _mainPanelSize - _unitSize);
                        soldier[i] = new PictureBox();
                        soldier[i].Size = new Size(_unitSize, _unitSize);
                        soldier[i].Location = _scenarioSoldiers[i];//new Point(0, randomSoldierY);
                        soldier[i].SizeMode = PictureBoxSizeMode.CenterImage;
                        soldier[i].Image = PathPlanning.Properties.Resources.soldier;
                        panelGodView.Controls.Add(soldier[i]);

                        soldierSight[i] = new Rectangle();
                        int soldierSightX = soldier[i].Location.X - (((_soldierSight * 2) / (700 / _mainPanelSize)) - _unitSize) / 2;
                        int soldierSightY = soldier[i].Location.Y - (((_soldierSight * 2) / (700 / _mainPanelSize)) - _unitSize) / 2;
                        soldierSight[i].Location = new Point(soldierSightX, soldierSightY);
                        soldierSight[i].Size = new Size((_soldierSight * 2) / (700 / _mainPanelSize), (_soldierSight * 2) / (700 / _mainPanelSize));
                    }

                    //_currentSoldierSight = soldierSight[0];
                    _allSoldierSight = soldierSight;

                    _allSoldiers = soldier;
                    _currentSoldier = soldier[0];
                    soldierEyePictureBox.Location = new Point(soldier[0].Location.X, Convert.ToInt32((Convert.ToDouble(soldier[0].Location.Y) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))));
                    soldierEyePictureBox.Tag = "soldierEyePictureBox";
                    #endregion


                    Rectangle[] listOfAllObtacles = new Rectangle[_numberOfArcher + _numberOfTowers];
                    _listOfAllObstacles = listOfAllObtacles;
                    _allObstacles = new Rectangle[_numberOfTowers + _numberOfArcher];
                    //_numberOfObstacles = _numberOfArcher + _numberOfTowers;
                    _allArcherSightRange.CopyTo(_allObstacles, 0);
                    _allTowerSight.CopyTo(_allObstacles, _numberOfArcher);


                    this.Invalidate();
                }
                else
                {
                    MessageBox.Show("Wrong Input!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _archerHitRange = 35;
                    _archerSightRange = 70;
                }
            }
        }


        //https://www.flaticon.com/

        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case 0x84:
        //            base.WndProc(ref m);
        //            if ((int)m.Result == 0x1)
        //                m.Result = (IntPtr)0x2;
        //            return;
        //    }

        //    base.WndProc(ref m);
        //}
    }
}