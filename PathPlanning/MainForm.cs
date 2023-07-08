using PathPlanning.Utilities;

namespace PathFinding
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Variables
        public int _moveSoldierY;
        public int _moveSoldierX;

        public int _currentSoldierNumber;
        public int _currentSoldierX;
        public int _currentSoldierY;
        public int _currentHealth;

        public Rectangle[] _listOfKnownTowers;
        public PictureBox[] _listOfKnownArchers;
        public Rectangle[] _listOfAllObstacles;

        public int _knownTowerNumber = 0;
        public int _knownArcherNumber = 0;
        public int _knownObstacleNumber = 0;
        public bool _isSoldierAlreadyGoneToX = false;
        public bool _isSoldierStuck;
        public int _soldierStuckNumber = 0;

        public Random _randomSoldierY = new Random();
        public Random _randomTowerY = new Random();
        public Random _randomTowerX = new Random();
        public Random _randomArcherY = new Random();
        public Random _randomArcherX = new Random();
        public Random _randomTargetY = new Random();
        public Random _archerRandomMoveDirection = new Random();

        public int[] _archerDirection;
        public int[] _archerInitialDirection;

        public PictureBox _currentSoldier = new PictureBox();
        public PictureBox[] _allSoldiers;
        public PictureBox[] _allArchers;
        public PictureBox[] _allTowers;
        public PictureBox[] _archerForSoldierEye;

        public Rectangle[] _allTowerSight;
        public Rectangle[] _allSoldierSight;
        public Rectangle[] _allArcherSightRange;
        public Rectangle[] _allArcherHitRange;
        public Rectangle[] _allObstacles;

        public Pen _redPen = new Pen(Color.Red);
        public Pen _greenPen = new Pen(Color.Green);
        public Pen _blackPen = new Pen(Color.Black);
        public Pen _bluePen = new Pen(Color.Blue);

        public int _numberOfSoldiers;
        public int _numberOfTowers;
        public int _numberOfArcher;
        public int _soldierMaxMovement;
        public int _archerMaxMovement;

        public int _mainPanelSize;
        public int _soldiersEyePanelSize = 300;
        public int _unitSize;
        public int _targetSize;
        public int _soldierEyeTargetSize;
        public int _soldierEyeUnitSize;

        public int _towerSight;
        public int _soldierSight;
        public int _archerSightRange;
        public int _archerHitRange;

        public int _health;
        public int _towerHitPower;
        public int _archerHitPower;
        public int _minDistanceFromTower;
        public int _defaultStep;

        public int _targetX;
        public int _targetY;

        public Point _scenarioTargetLocation;
        public Point[] _scenarioSoldiers;
        public Point[] _scenarioArchers;
        public Point[] _scenarioTowers;
        public int[] _scenarioUserVariables;
        public int[] _scenarioArcherFirstDirection;
        #endregion


        private void Timer_Tick(object sender, EventArgs e)
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

            // archer patrolling
            for (int i = 0; i < _numberOfArcher; i++)
            {
                if (_listOfKnownArchers[i] != null)//soldiers eye map archer lcoation
                    _archerForSoldierEye[i].Location = new Point(Convert.ToInt32((Convert.ToDouble(_allArchers[i].Location.X) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))), Convert.ToInt32((Convert.ToDouble(_allArchers[i].Location.Y) / (Convert.ToDouble(_mainPanelSize) / Convert.ToDouble(_soldiersEyePanelSize)))));

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
                            ArcherGoUpDefaultStep(_allArchers[i]);

                    }
                    else if (_archerDirection[i] == 0)
                    {
                        if (_allArchers[i].Location.Y + _unitSize >= _mainPanelSize)
                        {
                            ArcherGoUpDefaultStep(_allArchers[i]);
                            _archerDirection[i] = 1;
                        }
                        else
                            ArcherGoDownDefaultStep(_allArchers[i]);
                    }
                }

            }

            // only hit tower
            for (int i = 0; i < _numberOfTowers; i++)
            {
                int towerX = _allTowers[i].Location.X;
                int towerY = _allTowers[i].Location.Y;
                Rectangle tower = new Rectangle(towerX, towerY, _unitSize, _unitSize);

                if (_allSoldierSight[_currentSoldierNumber].IntersectsWith(tower) == false && _listOfKnownTowers.Contains(_allTowerSight[i]) == false && _allTowerSight[i].IntersectsWith(soldierOneStepForward) == true)
                {
                    if (_currentHealth - _towerHitPower > 0)
                    {
                        _currentHealth -= _towerHitPower;
                        lblCurrentHealth.Text = _currentHealth.ToString();
                    }
                    else
                    {
                        mainTimer.Stop();
                        _allSoldiers[_currentSoldierNumber].Visible = false;
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
                    if (_listOfKnownTowers.Contains(_allTowerSight[i]) == true && _allTowerSight[i].IntersectsWith(soldierOneStepBefore) == true)
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
                }
                else if (_allSoldierSight[_currentSoldierNumber].IntersectsWith(tower) == true && _allTowerSight[i].IntersectsWith(soldierOneStepBefore) == true && _listOfKnownTowers.Contains(_allTowerSight[i]) == true) //2.20 07/06/23
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
            }

            // only hit archer
            for (int i = 0; i < _numberOfArcher; i++)
            {
                Rectangle archer = new Rectangle(_allArchers[i].Location.X, _allArchers[i].Location.Y, _unitSize, _unitSize);
                if (_listOfKnownArchers[i] == null && _allArcherHitRange[i].IntersectsWith(soldier) == true && _allSoldierSight[_currentSoldierNumber].IntersectsWith(archer) == false)
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
                        return;
                    }
                }
                if (_listOfKnownArchers[i] != null && _allArcherHitRange[i].IntersectsWith(soldier) == true)
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
                        return;
                    }
                }
            }

            // add tower to list
            for (int i = 0; i < _numberOfTowers; i++)
            {
                int towerX = _allTowers[i].Location.X;
                int towerY = _allTowers[i].Location.Y;
                Rectangle tower = new Rectangle(towerX, towerY, _unitSize, _unitSize);

                if (_allSoldierSight[_currentSoldierNumber].IntersectsWith(tower) == true && _listOfKnownTowers.Contains(_allTowerSight[i]) == false)
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

            //add archer to list
            for (int i = 0; i < _numberOfArcher; i++)
            {
                int archerX = _allArchers[i].Location.X;
                int archerY = _allArchers[i].Location.Y;
                Rectangle archer = new Rectangle(archerX, archerY, _unitSize, _unitSize);

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
            }

            //general archer
            for (int i = 0; i < _numberOfArcher; i++)
            {
                if (_listOfKnownArchers[i] != null)
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
            }

            //general tower
            for (int i = 0; i < _numberOfTowers; i++) //tower hits and soldier does not see
            {
                if (_listOfKnownTowers.Contains(_allTowerSight[i]) == true)
                {
                    if (_currentSoldier.Location.X + _unitSize >= _mainPanelSize)
                    {
                        UpOrDownSoldierAccordingToTarget();
                        return;
                    }
                    if (_allTowerSight[i].IntersectsWith(soldier) == true)
                    {
                        AvoidFromKnownTower(_allTowers[i]);
                        return;
                    }
                    if (_allTowerSight[i].IntersectsWith(soldierSight) == true && _allTowerSight[i].IntersectsWith(soldier) == false)
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

            MoveSoldier();
        }

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

                if (_isSoldierAlreadyGoneToX == false && _targetX >= _currentSoldierX + _unitSize)
                {
                    _moveSoldierX += _defaultStep;
                    StepSoldier(_moveSoldierX, _moveSoldierY);
                    _isSoldierAlreadyGoneToX = true;
                }
                else if (_isSoldierAlreadyGoneToX == true)
                {
                    if (_targetY - (_targetSize) > _currentSoldierY)
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
            if (_currentSoldier.Top >= attackerTower.Top) //soldier lower than tower        
            {
                if (_currentSoldier.Location.Y + _unitSize >= _mainPanelSize)
                    StepSoldier(_currentSoldierX + _soldierMaxMovement, _currentSoldier.Location.Y);
                else
                    StepSoldier(_currentSoldierX, _currentSoldierY + _soldierMaxMovement);
                return;
            }
            else if (_currentSoldier.Top < attackerTower.Top) //soldier upper than tower
            {
                if (_currentSoldier.Location.Y <= 0)
                    StepSoldier(_currentSoldierX + _soldierMaxMovement, _currentSoldier.Location.Y);
                else
                    StepSoldier(_currentSoldierX, _currentSoldierY - _soldierMaxMovement);
                return;
            }

            if (_currentSoldier.Right >= attackerTower.Right) // soldier at the left            
                StepSoldier(_currentSoldierX - _soldierMaxMovement, _currentSoldierY);
            else if (_currentSoldier.Right < attackerTower.Right) // soldier sağda           
                StepSoldier(_currentSoldierX + _soldierMaxMovement, _currentSoldierY);
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
                _moveSoldierX += _soldierMaxMovement;
                StepSoldier(_moveSoldierX, _allSoldiers[_currentSoldierNumber].Location.Y);
                return;
            }
            else if (_currentSoldier.Location.Y <= 0)
            {
                _moveSoldierX += _soldierMaxMovement;
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
                StepSoldier(_currentSoldierX - _soldierMaxMovement, _currentSoldierY);

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
            if (soldier.Top >= archer.Top)
                archer.Location = new Point(archer.Location.X, archer.Location.Y + _archerMaxMovement);
            else if (soldier.Top < archer.Top)
                archer.Location = new Point(archer.Location.X, archer.Location.Y - _archerMaxMovement);
        }

        private void ArcherGoUpDefaultStep(PictureBox archer)
        {
            archer.Location = new Point(archer.Location.X, archer.Location.Y - _defaultStep);
        }

        private void ArcherGoDownDefaultStep(PictureBox archer)
        {
            archer.Location = new Point(archer.Location.X, archer.Location.Y + _defaultStep);
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

        internal void RefreshGame()
        {
            Tools tools = new Tools(this);
            tools.Refresh();
        }

        private void btnSetUnits_Click_1(object sender, EventArgs e)
        {
            Tools tool = new Tools(this);
            tool.SetFirstLocation();
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
                mainTimer.Start();
        }

        private void btnStopAttack_Click(object sender, EventArgs e)
        {
            mainTimer.Stop();
        }

        private void btnSaveScenario_Click(object sender, EventArgs e)
        {
            Scenario scenario = new Scenario(this);
            scenario.SaveScenario(_scenarioTargetLocation, _scenarioSoldiers, _scenarioArchers, _scenarioTowers, _scenarioUserVariables, _scenarioArcherFirstDirection);
        }

        private void btnOpenScenario_Click(object sender, EventArgs e)
        {
            Scenario scenario = new Scenario(this);
            scenario.OpenScenario();
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.Location = new Point(1043, 0);
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.Location = new Point(1040, 0);
        }

        //https://www.flaticon.com/
    }
}