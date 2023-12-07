using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

// [�Q�l]
//  �R�K�l�u���O: uGUI �ŃX�N���v�g����{�^�����������@ https://baba-s.hatenablog.com/entry/2018/01/03/110100
//  UnityQuesion: How to trigger a button click from script https://answers.unity.com/questions/945299/how-to-trigger-a-button-click-from-script.html
//  Hatena: �{�^���� 1�񂾂����s����悤�ɂ��� �g�����\�b�h https://hacchi-man.hatenablog.com/entry/2020/12/12/220000

namespace nitou.UI {

    /// <summary>
    /// Button�̊g�����\�b�h�N���X
    /// </summary>
    public static partial class ButtonExtensions {

        /// ----------------------------------------------------------------------------
        // �{�^���̎��s

        /// <summary>
        /// �w�肵���{�^���C�x���g�����s����g�����\�b�h
        /// </summary>
        public static void Execute<T>(this Button @this, ExecuteEvents.EventFunction<T> eventFunctor)
            where T : IEventSystemHandler {

            ExecuteEvents.Execute(
                    target: @this.gameObject,
                    eventData: new PointerEventData(EventSystem.current),   // ��current�Ō��݂̃C�x���g�V�X�e�����擾�ł���
                    functor: eventFunctor
                );
        }


        /// ----------------------------------------------------------------------------
        // �C�x���g�̓o�^

        /// <summary>
        /// �C�x���g��o�^����g�����\�b�h
        /// </summary>
        public static IDisposable SetOnClickDestination(this Button @this, Action onClick) {
            return @this.onClick
                .AsObservable()
                .Subscribe(x => onClick.Invoke())
                .AddTo(@this);
        }

        /// <summary>
        /// �P�񂾂����s������C�x���g��o�^����g�����\�b�h
        /// </summary>
        public static void OneShot(this Button @this, System.Action action) {
            @this.onClick.AddListener(() => {
                action?.Invoke();
                @this.onClick.RemoveAllListeners();     // ���S�����ł��邱�Ƃɒ���
            });
        }
    }


}