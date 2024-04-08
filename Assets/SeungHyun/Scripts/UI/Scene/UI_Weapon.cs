using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class UI_Weapon : UI_Scene
    {
        enum GameObjects
        {
            MainWeapon,
            //SubWeapon,
            BulletText,
        }
        // 나중에 이미지 바꿀때 사용
        Image _mainWeaponImage;
       // Image _subWeaponImage;

        Text _bulletText;

        bool isReload = false;

        int _maxBullet = 50;
        int _bulletCount = 50;

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            Bind<GameObject>(typeof(GameObjects));
            GameObject mainWeapon = GetObject((int)GameObjects.MainWeapon);
            //GameObject subWeapon = GetObject((int)GameObjects.SubWeapon);
            GameObject BulletText = GetObject((int)GameObjects.BulletText);

            // 총알 나가는 거 구독 해주고
            //Managers.Input.BulletReduce -= BulletCount;
            //Managers.Input.BulletReduce += BulletCount;

            _mainWeaponImage = mainWeapon.GetComponentInChildren<Image>();
            //_subWeaponImage = subWeapon.GetComponentInChildren<Image>();    
            _bulletText = BulletText.GetComponent<Text>();


            _bulletText.text = $"{_bulletCount} / {_maxBullet}";
        }

        

        void BulletCount(int bulletnum)
        {
            if (!isReload)
            {
                _bulletCount -= bulletnum;

                if (_bulletCount == 0)
                {
                    StartCoroutine("ReloadBullet");
                }

                _bulletText.text = $"{_bulletCount} / {_maxBullet}";
            }
           

            
        }


        IEnumerator ReloadBullet()
        {
            isReload = true;
            _bulletCount = 50;
            yield return new WaitForSeconds(2f);
            isReload = false;

        }
    }



